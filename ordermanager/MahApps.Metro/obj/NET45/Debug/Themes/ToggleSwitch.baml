   M S B A M L   `   `   `  ����J  FMahApps.Metro, Version=0.0.0.999, Culture=neutral, PublicKeyToken=null?$clr-namespace:MahApps.Metro.ControlsMahApps.Metro.Controls  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089/    )MahApps.Metro.Controls.ToggleSwitchButton  �� 
TargetType �� Property �� Value   AccentColorBrush �� 
TargetType ��Name J� Opacity �� Padding'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState	   
Storyboard
 �� TargetProperty �� 
TargetName R� KeyTime  System.Windows.Visibility   Visible R� Value   Transitions%  System.Windows.VisualTransition   GeneratedDuration	   To   
Storyboard g� Duration	 W� To W� EasingFunction/  )System.Windows.Media.Animation.CircleEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode4  .System.Windows.Media.Animation.ExponentialEase   Exponent9	  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value	 t� To �Name � UseLayoutRounding ��Name  
GrayBrush9  
BlackBrush  
WhiteBrush)
   #MahApps.Metro.Controls.ToggleSwitch � RowDefinitions � ColumnDefinitions  ��Name! 
  Header" 
  HeaderTemplate#   Name$ 
  SwitchForeground%    SwitchForeground�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      1Controls$clr-namespace:MahApps.Metro.Controls  5      %�  '         '
  �    �� 5      "    6   .�5   
   ��  � �� .  6&   �� 5   
    �@  SwitchForeground# �   6-   �� 5   
    � �� 5      �� 5	      "   6"   .R�5
      �� $
 Root��6   #��z��5      #��z��5      # z 5      # z��5       5        5      CommonStates��. 5   "     
Normal��6.     5   "   Disabled��.	 5   &   �� .��5   *   T� $
 (UIElement.Visibility)�6H   $ disabled��6{   ..�5   .   i� $ 0��6E   # Z 6Q   5   +   T� 5   *   $
 (UIElement.Visibility)�6H   $ disabledTumb��6{   ..�5   .   i� $ 0��6E   # Z 6Q   5   +   5   '   5   #     5   "   	MouseOver��.	 5   &   �� .��5   *   T� $
 (UIElement.Visibility)�6H   $ hover��6{   ..�5   .   i� $ 0��6E   # Z 6Q   5    +   5!   '   5"   #     5#   "   Pressed��.	 5$   &   �� .��5%   *   T� $
 (UIElement.Visibility)�6H   $ pressed��6{   ..�5&   .   i� $ 0��6E   # Z 6Q   5'   +   5(   '   5)   #   5*        5+      CheckStates�� 5,   "     5-   &   $ 0:0:0.05D�67   $ 	Unchecked��6T     5.   &   $ 0:0:0.05D�67   $ Checked��6T   . 5/   *   �� .��50   .   W� $ 0:0:0.05D�6>   $ 47X�6R   $
 (TranslateTransform.X)�6Z   $ SwitchThumbTranslate��6�    51   2     52   6   $ EaseIn=�6A   54   /   55   +   56   '   . 58   "     Checked��.	 59   &   �� .��5:   *   W� $ BackgroundTranslation��6:   $
 (TranslateTransform.X)�5;      $ 0D�5<      $ 68X�5=       5>   .     5?   2   $ 15Q�6B   $ EaseOut=�6P   5A   +   U� 5B   *   $U
 O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6H   $ SwitchThumb��6�   .\�5C   .   	  $ 0��6C   $ 47Q�6O   5D   +   5E   '   5F   #     5G   "   Dragging��6.     5H   "   	Unchecked��.	 5I   &   �� .��5J   *   W� $ BackgroundTranslation��6:   $
 (TranslateTransform.X)�5K      $ 0D�5L      $ 0X�5M      T� 5N   *   $
 (UIElement.Visibility)�6H   $ border��6{   ..�5O   .   i� $ 0��6E   # Z 6Q   5P   +   t� 5Q   *   $ 0D�6=   $ 1X�6J   $
 (Border.Margin)�6Q   $ checked��6}   5R   '   5S   #   5T      ._�5V      � $ 
SwitchRoot��6   $
��Auto��6I   $
��Auto��6W    . ��63   Z� .+�5W      � $ SwitchTrack��6#   $
��Auto��68   .+�5X   "   � $ SwitchBottom��6'   $��25��6=   $��60��6I   .+�5Y   &   �� $ SwitchBackground��60   $
��Auto��5Z      $
��Auto��5[      $��Stretch=�5\      $��Stretch=�5]      z�5^   *   T� 5_   .   BackgroundTranslation��6A   5a   '   �� 5b   &   $��2q�6-   #���  6A   ._�5c   *   � $��2q�6/   .+�5d   .   �� $ checked��65   $��0q�6F   #���   6Z   $��0q�6�   �� 5e   .   $ border��65   $��0q�6E   #���  6Y   $y�	Collapsed=�6�   $��0q�6�   �� 5f   .   $ pressed��65   $��0q�6F   #��� ��6Z   $
 0.25Q�6�   $y�	Collapsed=�6�   $��0q�6�   �� 5g   .   $ hover��65   $��0q�6D   #��� ��6X   $
 0.15Q�6�   $y�	Collapsed=�6�   $��0q�6�   �� 5h   .   $ disabled��65   $��0q�6G   #��� ��6[   $	 0.5Q�6�   $y�	Collapsed=�6�   $��0q�6�   5i   +   5j   '   5k   #   �� 5l   "   $ SwitchThumb��6)   #��z��5m      $��0q�5n      $��0q�5o      $��13��5p      $
��Auto��5q      $
��Left=�5r      z�5s   &   V� 5t   *   .~�5u   .   �� �� 5v   .   �� 5w   .   T� 5x   .   SwitchThumbTranslate��6A   5y   +   ._�5{   &   � .+�5|   *   �� $ ThumbCenter��61   #��z��6F   $��0q�6q   #���  6�   �� 5}   *   $ disabledTumb��61   #��z��6G   $��0q�6r   #���  6�   $	 0.5Q�6�   $y�	Collapsed=�6�   5~   '   5   #   5�      5�      5�      5�      5�      5�      �� 5�      "  
 6   .�5�   
   ��  � �� . 6&   �� 5�   
    �@
 SwitchForeground# �   6-   �� 5�   
    � ��$
 Left=�67   �� 5�   
    � ��$	 Top=�65   �� 5�   
    � ��# �  6'   �� 5�   
    � �� 5�      �� 5�      " 
 6"   .R�5�      �� #��z��6   #��z��5�      # z��5�       5�        5�      CommonStates��. 5�   "     
Normal��6.     5�   "   Disabled��.	 5�   &   �� .��5�   *   W� $ Header��6:   $
 (UIElement.Opacity)�6Y   $ 0D�6�   $	 0.3X�6�   W� 5�   *   $ Content��6:   $
 (UIElement.Opacity)�6Z   $ 0D�6�   $	 0.3X�6�   W� 5�   *   $ Switch��6:   $
 (UIElement.Opacity)�6Y   $ 0D�6�   $
 0.75X�6�   5�   '   5�   #   5�      ._�5�      � $��12,5,0,0q�6    5�      �� 5�   "   $
��Auto �60   �� 5�   "   $
��Auto �60    5�      �� 5�   "   $��* �63   �� 5�   "   $
��Auto �63   .+�5�      �� $  Header��6-   $��Normal"�5�      #��z! 5�      #��z" 5�      $
��Left=�5�      ��.  $��0,0,0,5q�5�      $
 0.65Q�5�      $	��Top=�5�      $��3��6)   $��16(�6=   $��0q�6K   �� 5�      $  Content��6-   $��1��5�      #��z��5�      #��z��5�      #��z��5�      ��.  $��	-1,1,0,-7q�5�      #��z��5�      $��Segoe UI Semibold)�6P   $��14.667(�6o      5�      $# Switch��6:   $��1��5�      $��1��5�      #��z��5�      #% z$ 5�      $��Bottom=�5�      $��Right=�6,   $��1��6H   5�      5�      5�      5�      5�      5�      