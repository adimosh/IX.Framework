// <copyright file="UndoableUnitBlockTransaction{T}.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Collections.Generic;
using IX.Guaranteed;
using IX.Undoable;

namespace IX.Observable
{
    internal class UndoableUnitBlockTransaction<T> : OperationTransaction
    {
        private readonly ObservableCollectionBase<T> collectionBase;

        internal UndoableUnitBlockTransaction(ObservableCollectionBase<T> collectionBase)
        {
            this.collectionBase = collectionBase
#if DEBUG
                ?? throw new ArgumentNullException(nameof(collectionBase))
#endif
                ;

            this.AddRevertStep(
                (state) =>
                {
                    ((ObservableCollectionBase<T>)state).FailExplicitTransaction();
                },
                collectionBase);

            this.StateChanges = new BlockStateChange
            {
                StateChanges = new List<StateChange>(),
            };
        }

        internal BlockStateChange StateChanges { get; }

        protected override void WhenSuccessful() => this.collectionBase.FinishExplicitTransaction();
    }
}