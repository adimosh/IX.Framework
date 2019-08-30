// <copyright file="Unmanaged.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;

namespace IX.StandardExtensions.WPF
{
    internal static class Unmanaged
    {
        [DllImport("user32.dll")]
        internal static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetIconInfo(
            IntPtr hIcon,
            ref IconInfo pIconInfo);

        [DllImport("user32.dll")]
        private static extern bool DestroyIcon(IntPtr hIcon);

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter - naming convention ignored as these are interop structs
#pragma warning disable IDE0012 // Simplify Names

        // ReSharper disable InconsistentNaming

        /// <summary>
        ///     The interop version of the ICONINFO struct.
        /// </summary>
        [UsedImplicitly]
        public struct IconInfo
        {
            /// <summary>
            ///     Specifies whether this structure defines an icon or a cursor.
            ///     A value of TRUE specifies an icon; FALSE specifies a cursor.
            /// </summary>
            [UsedImplicitly]
            public bool fIcon;

            /// <summary>
            ///     The x-coordinate of a cursor's hot spot.
            /// </summary>
            [UsedImplicitly]
            public int xHotspot;

            /// <summary>
            ///     The y-coordinate of a cursor's hot spot.
            /// </summary>
            [UsedImplicitly]
            public int yHotspot;

            /// <summary>
            ///     The icon bitmask bitmap.
            /// </summary>
            [UsedImplicitly]
            public IntPtr hbmMask;

            /// <summary>
            ///     A handle to the icon color bitmap.
            /// </summary>
            [UsedImplicitly]
            public IntPtr hbmColor;
        }

        // ReSharper restore InconsistentNaming
#pragma warning restore IDE0012 // Simplify Names
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

        [UsedImplicitly]
        internal class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            public SafeIconHandle()
                : base(true)
            {
            }

            protected override bool ReleaseHandle() => DestroyIcon(this.handle);
        }
    }
}