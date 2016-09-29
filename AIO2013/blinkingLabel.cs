using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace AIO2013
{
    class BlinkLabel : Label
    {
        private const int _maxNumberOfBlinks = 2 * 3;
        private int _blinkCount = 0;
        private Timer _timer;
        private Color oldColor;

        public BlinkLabel()
        {
            this._timer = new Timer();
            this._timer.Tick += new EventHandler(_timer_Tick);
            this._timer.Interval = 621;
        }

        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            if (!this._timer.Enabled && base.IsHandleCreated) StartBlink();
        }

        public void StartBlink()
        {
            this._blinkCount = 0;
            base.Visible = true;
            this.oldColor = base.ForeColor;
            base.ForeColor = System.Drawing.Color.Purple;
            this._timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (base.ForeColor == System.Drawing.Color.Purple)
            {
                base.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                base.ForeColor = System.Drawing.Color.Purple;
            }

            this._blinkCount++;

        }

        public void StopBlink()
        {
            this._timer.Stop();
            base.Visible = true;
            base.ForeColor = oldColor;
        }
    }
}
    