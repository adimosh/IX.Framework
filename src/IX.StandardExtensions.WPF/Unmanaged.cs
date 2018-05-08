// <copyright file="Unmanaged.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace IX.StandardExtensions.WPF
{
    internal static class Unmanaged
    {
        [DllImport("user32.dll")]
        internal static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter - naming convention ignored as these are interop structs
#pragma warning disable SA1121 // Use built-in type alias
#pragma warning disable IDE0012 // Simplify Names
        /// <summary>
        /// The interop version of the ICONINFO struct.
        /// </summary>
        public struct IconInfo
        {
            /// <summary>
            /// Specifies whether this structure defines an icon or a cursor.
            /// A value of TRUE specifies an icon; FALSE specifies a cursor.
            /// </summary>
            public bool fIcon;

            /// <summary>
            /// The x-coordinate of a cursor's hot spot.
            /// </summary>
            public Int32 xHotspot;

            /// <summary>
            /// The y-coordinate of a cursor's hot spot.
            /// </summary>
            public Int32 yHotspot;

            /// <summary>
            /// The icon bitmask bitmap.
            /// </summary>
            public IntPtr hbmMask;

            /// <summary>
            /// A handle to the icon color bitmap.
            /// </summary>
            public IntPtr hbmColor;
        }
#pragma warning restore IDE0012 // Simplify Names
#pragma warning restore SA1121 // Use built-in type alias
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter

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