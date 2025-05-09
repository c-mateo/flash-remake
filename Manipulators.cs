using System.Diagnostics;

namespace Vector_Meshes
{
    public class Manipulators
    {
        ControlPoint _main;
        ControlPoint? _handle;

        public ControlPoint Main
        {
            get => _main;
            set
            {
                _main.MouseDown -= Main_MouseDown;
                _main.LostFocus -= Main_LostFocus;
                _main = value;
                _main.MouseDown += Main_MouseDown;
                _main.LostFocus += Main_LostFocus;
            }
        }

        public ControlPoint? Handle
        {
            get => _handle;
            set
            {
                if (_handle != null)
                {
                    _handle.VisibleChanged -= Handle_VisibleChanged;
                    _handle.LostFocus -= Handle_LostFocus;
                    _handle.MouseDown -= Handle_MouseDown;
                }
                _handle = value;
                if (_handle != null) { 
                    _handle.VisibleChanged += Handle_VisibleChanged;
                    _handle.LostFocus += Handle_LostFocus;
                    _handle.MouseDown += Handle_MouseDown;
                }
            }
        }

        void Main_MouseDown(object? sender, MouseEventArgs e)
        {
            if (_handle != null)
            {
                if (e.Clicks == 2)
                {
                    Form1.context?.Controls.Remove(_handle);
                    _handle = null;
                }
                else
                {
                    _handle.Visible = true;
                    _handle.Focus();
                }
            }
            else if (e.Clicks == 2)
            {
                var handle = Form1.context?.AddPoint(Main.Position, e.Location);
                if (handle != null)
                {
                    Handle = handle;
                    //handle.Visible = true;
                    //handle.Focus();
                    //Handle.Capture = true;
                }
            }
        }

        private void Main_LostFocus(object? sender, EventArgs e)
        {
            if (Handle != null && !Handle.Focused)
            {
                Handle.Visible = false;
            }
        }
        
        private void Handle_LostFocus(object? sender, EventArgs e)
        {
            if (Handle != null && !Main.Focused) {
                Handle.Visible = false;
            }
        }

        void Handle_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                Form1.context?.Controls.Remove(_handle);
                _handle = null;
            }
        }

                //private void Handle_GotFocus(object? sender, EventArgs e)
                //{
                //    if (Handle != null)
                //    {
                //        Debug.WriteLine("Got focus");
                //        //Handle.Visible = true;
                //        //Form1.context?.Invalidate();
                //    }
                //}

                static void Handle_VisibleChanged(object? sender, EventArgs e)
        {
            Form1.context?.Invalidate();
        }

        public Manipulators(ControlPoint anchor, ControlPoint? handle = null)
        {
            _main = anchor;
            _main.MouseDown += Main_MouseDown;
            _main.LostFocus += Main_LostFocus;
            Handle = handle;
        }

        public void Draw(Graphics g)
        {
            if (Handle is { Visible: true })
            {
                g.DrawLine(Pens.Blue, Main.Position, Handle.Position);
            }
        }
    }

}
