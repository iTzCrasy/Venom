using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Venom.Domain
{
    public class TrayIcon
    {
        private NotifyIcon _notifyIcon = new NotifyIcon( );
        public TrayIcon()
        {
            _notifyIcon.Icon = new Icon( "Venom.Ico" );
            _notifyIcon.Visible = true;
        }

        public void ShowInfo( string title, string message ) => _notifyIcon.ShowBalloonTip( 5000, title, message, ToolTipIcon.Info );
        public void ShowError( string title, string message ) => _notifyIcon.ShowBalloonTip( 5000, title, message, ToolTipIcon.Error );
    }
}
