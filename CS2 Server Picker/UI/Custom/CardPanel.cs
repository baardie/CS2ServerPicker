using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CS2_Server_Picker.UI.Custom
{
    /// <summary>
    /// A custom CS2-themed panel with rounded corners, tactical colors, and subtle shadow.
    /// </summary>
    public class CardPanel : Panel
    {
        // Default CS2-style properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; } = 10;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.FromArgb(255, 255, 128, 0); // CS2 orange accent

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int BorderThickness { get; set; } = 2;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color FillColor { get; set; } = Color.FromArgb(30, 30, 30); // dark steel background

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ShadowSize { get; set; } = 6;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ShadowColor { get; set; } = Color.FromArgb(100, 0, 0, 0); // subtle dark shadow

        /// <summary>
        /// Custom paint logic for CS2-themed rounded card.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Define drawing bounds with shadow offset
            var rect = new Rectangle(
                ShadowSize,
                ShadowSize,
                Width - ShadowSize * 2,
                Height - ShadowSize * 2);

            using var path = RoundedRect(rect, CornerRadius);

            // Draw soft shadow behind the panel
            using (var shadowBrush = new PathGradientBrush(path)
            {
                CenterColor = ShadowColor,
                SurroundColors = new[] { Color.Transparent }
            })
            {
                g.FillPath(shadowBrush, path);
            }

            // Fill background
            using (var brush = new SolidBrush(FillColor))
            {
                g.FillPath(brush, path);
            }

            // Draw CS2 orange border
            using (var pen = new Pen(BorderColor, BorderThickness))
            {
                g.DrawPath(pen, path);
            }
        }

        /// <summary>
        /// Creates a rounded rectangle path for the given bounds and radius.
        /// </summary>
        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90); // Top-left
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90); // Top-right
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90); // Bottom-right
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90); // Bottom-left
            path.CloseFigure();
            return path;
        }
    }
}
