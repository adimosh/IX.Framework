// <copyright file="ObservableListCapturedItemsUnitTests.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using Xunit;

namespace IX.Observable.UnitTests
{
    /// <summary>
    /// Captured items test for ObservableList
    /// </summary>
    public class ObservableListCapturedItemsUnitTests
    {
        /// <summary>
        /// The first test in the suite.
        /// </summary>
        [Fact(DisplayName = "ObservableList captured item undo/redo")]
        public void Test1()
        {
            // ARRANGE
            var item1 = new CapturedItem();

            var list = new ObservableList<CapturedItem>
            {
                AutomaticallyCaptureSubItems = true,
            };

            // ACT
            list.Add(item1);

            item1.TestProperty = "aaa";
            item1.TestProperty = "bbb";
            item1.TestProperty = "ccc";

            list.Undo();

            // ASSERT
            Assert.Equal("bbb", item1.TestProperty);
        }
    }
}