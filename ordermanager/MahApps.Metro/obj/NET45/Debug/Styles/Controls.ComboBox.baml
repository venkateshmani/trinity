   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      J FMahApps.Metro, Version=0.0.0.999, Culture=neutral, PublicKeyToken=null?$clr-namespace:MahApps.Metro.ControlsMahApps.Metro.Controls 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   MetroComboBox �� Property �� Value  	TextBrush 	 Gray7 �� Color  TextBoxBorderBrush  ContentFontFamily  ContentFontSize �� 
TargetType � 	Resources  Key  
GrayBrush7'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard	 �� TargetProperty
 �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value ��Name ��Name  
WhiteBrush J� IsHitTestVisible J� Opacity 	 
GrayBrush8 
 
GrayBrush5 $  TextBoxMouseOverInnerBorderBrush  TextBoxMouseOverBorderBrush  HighlightBrush J� SnapsToDevicePixels g� Duration R� KeyTime R� Value  System.Windows.Visibility d�Name  comboToggleStyle-  'System.Windows.Input.KeyboardNavigation   	IsTabStop d� 	IsChecked �� Mode �� RelativeSource F�Name �� Stretch 	 Gray3  ControlBackgroundBrush ��Name*  $MahApps.Metro.Controls.TextboxHelper   	Watermark �� 
IsReadOnly �� 
IsReadOnly  ��Name! �� SelectionBoxItemTemplate" �� SelectionBoxItem## �� SelectionBoxItemStringFormat$ �� ContentStringFormat  DisabledWhiteColorBrush  ControlsValidationBrush% ^� ToolTip& _�Name' _� 	Placement  ValidationToolTipTemplate( � DataContext) _� PlacementTarget* � Triggers+ :� RoutedEvent  ValidationBrush5  WhiteColorBrush, �Name  DropShadowBrush- J� Effect  ComboBoxPopupBrush. ��Name/ ��Name0   DirectionalNavigation1 �� Triggers2 P� Property3 P� Value4 �� 
TargetName  MetroComboBoxItem  NormalForegroundColor  TransparentWhiteBrush  AccentColorBrush  AccentColor3  AccentSelectedColorBrush�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      1System&clr-namespace:System;assembly=mscorlib  5      1Controls$clr-namespace:MahApps.Metro.Controls  5      %�$  &	        &	 �    �� 5      "  ��6   .�5   
   ��  � ��$ 26��6&   �� 5   
    � ��# �  6'   �� 5	   
    � �� 5
      �� 5      # �  6"   5      �� 5   
    � ��$
 Left=�67   �� 5   
    � ��$ Center=�65   �� 5   
    � ��$ 1q�6,   �� 5   
    � ��$
 Auto=�6G   �� 5   
    � ��$
 Auto=�6E   �� 5   
    � ��# �  6(   �� 5   
    � ��# �  6'   �� 5   
    � ��# �  6%   �� 5   
    �@J�SnapsToDevicePixels . 60   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �  5      ���� 5      comboToggleStyle "  d�6=   .�5   "   ��  � ��# �  6?   �� 5    "    � ��# �  6?   �� 5!   "    � ��# �  6@   �� 5"   "    � ��$ 1q�6D   �� 5#   "    � ��$ 3q�6<   �� 5$   "    � �� 5%   &   �� 5&   *   " d�6:   .R�5'   .   �  5(   2     5)   6   CommonStates��. 5*   :     
Normal��6F     5+   :   	MouseOver��. 5,   >   �� .��5-   B   U� $	 (UIElement.Opacity)�6`   $
 MouseOverBorder��6�   .\�5.   F     $ 0��6[   $ 1Q�6g   5/   C   50   ?   51   ;     52   :   Pressed��. 53   >   �� .��54   B   U� $	 (UIElement.Opacity)�6`   $
 PressedBorder��6�   .\�55   F     $ 0��6[   $ 1Q�6g   56   C   57   ?   58   ;     59   :   Disabled��6F   5:   7     5;   6   CheckStates��. 5<   :     Checked��. 5=   >   �� .��5>   B   U� $	 (UIElement.Opacity)�6`   $
 CheckedRectangle��5?   `   .\�5@   F     $ 0:0:0.1��6[   $ 1Q�6m   5A   C   5B   ?   5C   ;     5D   :   	Unchecked��6F   5E   7     5F   6   FocusStates��. 5G   :     Focused��. 5H   >   �� .��5I   B   U� $	 (UIElement.Opacity)�6`   $
 FocusRectangle��5J   `   .\�5K   F     $ 0��6[   $ 1Q�6g   5L   C   5M   ?   5N   ;     5O   :   	Unfocused��6F   5P   7   .+�5R   2   �� $ 
Background��69   #��z��6M   #��z��6y   #��z��6�   �� 5S   2   $ DisabledVisualElement��6<   #���  6[    .  $ 0Q�6�   �� 5T   2   $ MouseOverBorder��69   #��� 	 6R   #��z��6|   $ 0Q�6�   �� 5U   2   $ PressedBorder��69   #��� 
 6P   #��z��6z   $ 0Q�6�   �� 5V   2   $ FocusRectangle��6<   #���  6T   $ 0Q�6�   �� 5W   2   $ CheckedRectangle��6<   #��z��6V   $ 0Q�6�   #���  6�   �� 5X   2   $ FocusVisualElement��6<    .  $��1q�6q   #���  6|   $��1��6�   $y�	Collapsed=�6�   �� 5Y   2   #��z��6C    .  #��z��5[   C   # z 5\   C   #��z��5]   C   5^   /   5_   +   5a   #   5b      5c       5d        5e      CommonStates��. 5f   "     
Normal��6.     5g   "   	MouseOver��6.     5h   "   Disabled��. 5i   &   �� .��5j   *   U� $	 (UIElement.Opacity)�6H   $
 DisabledVisualElement��6x   .\�5k   .   �� $ 00:00:00��6C   $
 0.45Q�6V   5l   +   5m   '   5n   #   5o        5p      FocusStates��. 5q   "     Focused��. 5r   &   �� .��5s   *   U� $	 (UIElement.Opacity)�6H   $
 FocusRectangle��6x   .\�5t   .     $ 0��6C   $ 1Q�6O   5u   +   5v   '   5w   #     5x   "   	Unfocused��6.     5y   "   FocusedDropDown��. 5z   &   �� .��5{   *   T� $ 00:00:00D�6H   $	 (UIElement.Visibility)�6\   $
 PopupBorder��6�   ..�5|   .   i� $ 00:00:00��6E    5}   2    5~   6   Visible=�6J   5�   /   5�   +   5�   '   5�   #   5�        5�      ValidationStates��. 5�   "     	Valid��6.     5�   "   InvalidUnfocused��. 5�   &   �� .��5�   *   T� $	 
Visibility�6H   $
 ValidationErrorElement��6o   ..�5�   .   i� $ 0��6E    5�   2    5�   6   Visible=�6J   5�   /   5�   +   5�   '   5�   #     5�   "   InvalidFocused��. 5�   &   �� .��5�   *   T� $	 
Visibility�6H   $
 ValidationErrorElement��6o   ..�5�   .   i� $ 0��6E    5�   2    5�   6   Visible=�6J   5�   /   5�   +   T� 5�   *   $	 IsOpen�6H   $
 validationTooltip��6k   ..�5�   .   i� $ 0��6E    5�   2   ��5�   6   True��6K   5�   /   5�   +   5�   '   5�   #   5�      .+�5�      �� $ ContentPresenterBorder��6!   ._�5�      � .+�5�   "   d� $ DropDownToggle��6/   #��z��5�   /   #��z��5�   /   #��z��5�   /   $��Stretch=�5�   /   $��Right=�5�   /   $��0q�5�   /   #���  5�   /   $��Stretch=�5�   /    .   5�   /   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+.��5�   &   F� $ BtnArrow��6+   +���� ��v���P.��v���8�BІ�� ��v��$��Right=�5�   +   $��4��5�   +   $��0,0,6,0q�5�   +   $ Uniform=�5�   +   $��8��5�   +   ��5�   *   �� 5�   .   BtnArrowColor��# �  6U   5�   '   5�   #   �� 5�   "   $ EditableTextBoxBorder��6)   $y�	Collapsed=�5�   -   #��z��5�   -   $��1,1,0,1q�5�   -   #���  5�   -   $��Stretch=�5�   -   $��0,0,23,0q�5�   -   $��Stretch=�5�   -   ._�5�   *   �� $ PART_EditableTextBox��65   $��Stretch=�5�   5   #��z��5�   5   # z 5�   5   #��z��5�   5   $��0q�5�   5   # z 5�   5   #��z��5�   5   |�. # z 5�   5   #��z��5�   5   #��z��5�   5   ��5�   5   Z� 5�   5   5�   #   � 5�   "   $��4 0 16 0q�6'   .+�5�   &   �� $  ContentSite��67   #��z! 6L   #��z��6�   #��z" 6�   #$ z# 6�   #��z��6<   .  #��z��6�  # z 6�  #��z��6�  5�   #   5�      5�      �� 5�      $ FocusRectangle��6$   #���  6<   $ 0Q�6x   �� 5�      $ DisabledVisualElement��6$   #���  6C    .  $ 0Q�6�   �� 5�      $ ValidationErrorElement��6!   #���  6A   $��1q�6y   $y�	Collapsed=�6�   % 5�      _� 5�   "   $& validationTooltip��6*   $' Right=�5�   *   #���  5�   *   ( 5�   *   ��  �� *TemplatedParent+) 5�   *   ��  �� *TemplatedParent+* 5�   &   :� 5�   *   $+ Canvas.Loaded��67   .[�5�   .   �� .��5�   2   �� .��5�   6   T� $	 IsHitTestVisible�6T   ..�5�   :   i� $ 0��6Q    5�   >   ��5�   B   true��6W   5�   ;   5�   7   5�   3   5�   /   5�   +   5�   #   ._�5�      � 
������ $��Right=�5�   #   $��12��5�   #   $��	1,-4,-4,0q�5�   #   $	��Top=�5�   #   $��12��5�   #   .+�5�   "   F� ;����@B     р�[     � z �� ��� �� �J]� z ��j 	#���  6Q   $��1,3,0,0q�6{   F� 5�   "   +����        р�     � z ��[ � z  z 	#���  6D   $��1,3,0,0q�6m   5�      5�      � 5�      $, 
PART_Popup��6    #��� .�5      $��Bottom=�5      ��5       �� *IsDropDownOpen+ �� *TemplatedParent+��5      �� *ActualWidth+ �� *TemplatedParent+��5      �� *MaxDropDownHeight+ �� *TemplatedParent+.��5     �� $ PopupBorder��6%   #- �  5  %   #��z��5  %   $��Stretch=�5  %   $
��Auto��5	  %   #���  5
  %   #���  5  %   ._�5  "   �� $. DropDownScrollViewer��6/   $��0q�6M   $��2q�6a   $��1q�6l   .��5  &   �� $/ ItemsPresenter��65   $0 	Contained=�6M   5  #   5     5     5     1 5     P� 5     2 �@��
IsEditable3 . . �5     ��  � �� .  6:   �� 5     $4 EditableTextBoxBorder��6%    � y�$ Visible=�6^   �� 5     $4 ContentSite��6%    � y�$ Hidden=�6T   �� 5     $4 DropDownToggle��6%    � |� .  6V   �� 5     $4 DropDownToggle��6%    � ��$ Right=�6`   �� 5     $4 DropDownToggle��6%    � ��
 ���� 6W   �� 5     $4 DropDownToggle��6%    � ��$ 23��6R   �� 5     $4 DropDownToggle��6%    � ��$ 0,1,1,1q�6\   5     5     5      5!     �� 5#     "  ��6   .�5$  
   ��  � �� 5%     �� 5&     # �  6"   5(     �� 5)  
    � ��$ 2q�6$   �� 5*  
    � ��$
 Left=�67   �� 5+  
    � ��$	 Top=�65   �� 5,  
    � ��# �  6'   �� 5-  
    � ��$ 1q�6,   �� 5.  
    � �� 5/     �� 50     " ��6"   .R�51     � #��z��6   $��0,0.5q�6E    52       53     CommonStates��. 54  "     
Normal��6.     55  "   	MouseOver��. 56  &   �� .��57  *   U� $	 (UIElement.Opacity)�6H   $
 MouseOverRectangle��6x   .\�58  .     $ 0:0:0.1��6C   $	 .65Q�6U   59  +   5:  '   5;  #     5<  "   Disabled��. 5=  &   �� .��5>  *   U� $	 Opacity�6H   $
 contentPresenter��6l   .\�5?  .   �� $ 0��6C   $	 .55Q�6O   5@  +   5A  '   5B  #   5C       5D     SelectionStates��. 5E  "     
Unselected��6.     5F  "   Selected��. 5G  &   �� .��5H  *   U� $	 (UIElement.Opacity)�6H   $
 SelectedRectangle��6x   .\�5I  .     $ 0:0:0.1��6C   $ 1Q�6U   5J  +   5K  '   5L  #   5M       5N     FocusStates��. 5O  "     Focused��. 5P  &   �� 5Q  #     5R  "   	Unfocused��6.   5S     .+�5U     �� $ SelectedRectangle��6$    .  $ 0Q�6X   #���  6d   �� 5V     $ MouseOverRectangle��6$    .  $ 0Q�6Y   ��5W     �� 5X  "   # �  62   5Z     �� 5[     #��z��6)   .��5\     �� $  contentPresenter��6/   #��z��6I   #��z��6l   5]     �� 5^     $ FocusVisualElement��6$   #���  6@   $��1��6j   $y�	Collapsed=�6~   5_     1 5a     P� 5b     2 �@��
IsSelected3 . . �5c     �� # �  6;    � ��5d     5f     5h     5i     5j     