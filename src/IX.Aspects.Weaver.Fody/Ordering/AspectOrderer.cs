using System;
using System.Collections.Generic;
using System.Linq;

namespace IX.Aspects.Weaver.Fody.Ordering
{
    public static class AspectOrderer
    {
        public static List<AspectInfo> Order(List<AspectInfo> aspectInfos)
        {
            var aspectInfosWithOrderAttributes = aspectInfos
                .Where(x => x.AspectRoleDependencyAttributes.Count > 0)
                .ToList();

            if (aspectInfosWithOrderAttributes.Count == 0)
                return aspectInfos;

            var orderedAspects = OrderInternal(aspectInfosWithOrderAttributes);
            var aspectsWithoutOrderAttribute = aspectInfos.Except(aspectInfosWithOrderAttributes);

            return orderedAspects
                .Concat(aspectsWithoutOrderAttribute)
                .ToList();
        }

        private static IEnumerable<AspectInfo> OrderInternal(List<AspectInfo> aspectInfos)
        {
            var allUsedRoles = aspectInfos
                .Select(x => x.Role)
                .Distinct()
                .ToList();

            var resultRoleOrder = allUsedRoles.ToArray();
            foreach (var aspectInfo in aspectInfos)
                Array.Sort(resultRoleOrder, new RoleOrderComparer(aspectInfo));

            // verify
            var referenceOrderString = string.Join("_", resultRoleOrder);
            var tmpOrder = resultRoleOrder.ToArray();
            foreach (var aspectInfo in aspectInfos)
            {
                Array.Sort(tmpOrder, new RoleOrderComparer(aspectInfo));

                var currentOrderString = string.Join("_", tmpOrder);
                if (referenceOrderString != currentOrderString)
                    throw new InvalidAspectConfigurationException(
                        string.Format("No valid aspect ordering could be created for aspect '{0}'", aspectInfo.Name));
            }

            var roleIndexes = resultRoleOrder
                .Select((x, i) => new RoleIndex
                {
                    Index = i,
                    Role = x
                })
                .ToDictionary(x => x.Role, x => x.Index);

            var orderedAspects = aspectInfos
                .OrderBy(x => x.Order.GetOrderIndex(roleIndexes))
                .ToList();
            return orderedAspects;
        }
    }
}