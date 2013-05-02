using System.ComponentModel;
using System.Drawing;

namespace Bng.EL2SL.Configurator
{
    public class VerticalLabel : System.Windows.Forms.Control
    {

        private string _labelText;
        private System.ComponentModel.Container components;

        #region default constructor, destructor and initialisation code
        public VerticalLabel()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Color labelColor = this.BackColor;
            Pen labelBorderPen = new Pen(labelColor, 0);
            SolidBrush labelBackColorBrush = new SolidBrush(labelColor);
            SolidBrush labelForeColorBrush = new SolidBrush(base.ForeColor);

            float sngControlWidth = Width;
            float sngControlHeight = Height;
            e.Graphics.DrawRectangle(labelBorderPen, 0, 0, sngControlWidth, sngControlHeight);
            e.Graphics.FillRectangle(labelBackColorBrush, 0, 0, sngControlWidth, sngControlHeight);
            float sngTransformX = 0;
            float sngTransformY = sngControlHeight;
            e.Graphics.TranslateTransform(sngTransformX, sngTransformY);
            e.Graphics.RotateTransform(270);

            e.Graphics.DrawString(_labelText, Font, labelForeColorBrush, 0, 0);
            base.OnPaint(e);
        }

        #region windows form designer support
        [Category("Verticallabel"), Description("Text is displayed vertiaclly in container")]
        public override string Text
        {
            get
            {
                return _labelText;
            }
            set
            {
                _labelText = value;
                SizeF sf = CreateGraphics().MeasureString(this._labelText, Font);
                Size = new Size((int)sf.Height, (int)sf.Width);
                Invalidate();
            }
        }
        #endregion



    }
}