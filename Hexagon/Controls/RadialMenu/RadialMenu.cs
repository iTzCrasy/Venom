using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Hexagon.Controls
{
    public class RadialMenu : ContentControl
    {
        #region Dependency
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register( "IsOpen", typeof( bool ), typeof( RadialMenu ),
            new FrameworkPropertyMetadata( false, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure ) );

        public new static readonly DependencyProperty ContentProperty = DependencyProperty.Register( "Content", typeof( List<RadialMenuItem> ), typeof( RadialMenu ),
            new FrameworkPropertyMetadata( null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure ) );

        public static readonly DependencyProperty CentralItemProperty = DependencyProperty.Register( "CentralItem", typeof( RadialMenuCentralItem ), typeof( RadialMenu ),
            new FrameworkPropertyMetadata( null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure ) );
        #endregion

        #region Dependency Properties
        public bool IsOpen
        {
            get => ( bool )GetValue( IsOpenProperty );
            set => SetValue( IsOpenProperty, value );
        }
        public new List<RadialMenuItem> Content
        {
            get => ( List<RadialMenuItem> )GetValue( ContentProperty ); 
            set => SetValue( ContentProperty, value ); 
        }

        public RadialMenuCentralItem CentralItem
        {
            get => ( RadialMenuCentralItem )GetValue( CentralItemProperty ); 
            set => SetValue( CentralItemProperty, value ); 
        }
        #endregion

        static RadialMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( RadialMenu ), new FrameworkPropertyMetadata( typeof( RadialMenu ) ) );
        }

        public override void BeginInit( )
        {
            Content = new List<RadialMenuItem>( );
            base.BeginInit( );
        }
    }
}
