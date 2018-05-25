// <copyright file="UIElementExtensions.cs" company="Adrian Mos">
// Copyright (c) Adrian Mos with all rights reserved. Part of the IX Framework.
// </copyright>

using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IX.StandardExtensions.WPF.Extensions
{
    /// <summary>
    /// Extensions for <see cref="UIElement"/>.
    /// </summary>
    public static class UIElementExtensions
    {
        /// <summary>
        /// Creates a cursor from an element.
        /// </summary>
        /// <param name="element">The element to create the cursor from.</param>
        /// <param name="xPosition">The X-axis position of the cursor hot-spot.</param>
        /// <param name="yPosition">The Y-axis position of the cursor hot-spot.</param>
        /// <returns>A cursor looking like the element.</returns>
        public static Cursor CreateCursorFromElement(this UIElement element, int xPosition, int yPosition)
        {
            using (Bitmap bitmap = CreateImageFromElement(element))
            {
                return CreateCursorByUnmanaged(bitmap, xPosition, yPosition);
            }
        }

        /// <summary>
        /// Creates an image from an element.
        /// </summary>
        /// <param name="element">The element to create the image from.</param>
        /// <returns>A <see cref="Bitmap"/> with the exact rendered view of the element.</returns>
        public static Bitmap CreateImageFromElement(this UIElement element)
        {
            // Argument validation
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            // Measure and arrange the element
            element.Measure(new global::System.Windows.Size(element.RenderSize.Width - 1, element.RenderSize.Height - 1));
            element.Arrange(new Rect(default, element.DesiredSize));

            // Get DPI scale matrix
            Matrix dpiMatrix = PresentationSource.FromVisual(element).CompositionTarget.TransformToDevice;

            try
            {
                var encoder = new PngBitmapEncoder();

                // Prepare element for image rendering
                var rtb = new RenderTargetBitmap((int)element.DesiredSize.Width, (int)element.DesiredSize.Height, dpiMatrix.M11 * 96D, dpiMatrix.M22 * 96D, PixelFormats.Pbgra32);
                rtb.Render(element);

                // Create one frame in the encoder
                encoder.Frames.Add(BitmapFrame.Create(rtb));

                // Save bitmap
                using (var memoryStream = new global::System.IO.MemoryStream())
                {
                    encoder.Save(memoryStream);

                    return new Bitmap(memoryStream);
                }
            }
            catch
            {
                // If there is any error in the process, the element cursor is replaced with the default
#pragma warning disable ERP022 // Catching everything considered harmful. - This is acceptable, as we're talking about a fully-
                return null;
#pragma warning restore ERP022 // Catching everything considered harmful.
            }
        }

        private static Cursor CreateCursorByUnmanaged(Bitmap bitmap, int xPosition, int yPosition)
        {
            var unmanagedIconInfo = default(Unmanaged.IconInfo);

            Unmanaged.GetIconInfo(bitmap.GetHicon(), ref unmanagedIconInfo);

            unmanagedIconInfo.xHotspot = xPosition;
            unmanagedIconInfo.yHotspot = yPosition;
            unmanagedIconInfo.fIcon = false;

            Unmanaged.SafeIconHandle cursorHandle = Unmanaged.CreateIconIndirect(ref unmanagedIconInfo);
            return CursorInteropHelper.Create(cursorHandle);
        }
    }
}