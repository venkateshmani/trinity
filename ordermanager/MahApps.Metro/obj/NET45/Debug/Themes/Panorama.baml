   M S B A M L   `   `   `  ����J  FMahApps.Metro, Version=0.0.0.999, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MahApps.Metro.ConvertersMahApps.Metro.Converters  5      ?$clr-namespace:MahApps.Metro.ControlsMahApps.Metro.Controls  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:    4MahApps.Metro.Converters.PanoramaGroupWidthConverter   conv;   5MahApps.Metro.Converters.PanoramaGroupHeightConverter   PanoramaGroupHeightConverter;   5MahApps.Metro.Converters.PanoramaGroupHeaderConverter   PanoramaGroupHeaderConverter  headerLabelStyle  �� 
TargetType �� Property �� Value �� RelativeSource �� AncestorType%   MahApps.Metro.Controls.Panorama �� Mode �� Path �� Triggers �� Binding	 �� Value
 `� 	Converter �� DataType*   $MahApps.Metro.Controls.PanoramaGroup � 	Resources  Key �� 	Resources �� Color ��Name ��Name �� SelectionMode$ �� IsSynchronizedWithCurrentItem  transparentListBoxItemStyle �� ElementName �� 
TargetType ��Name J� SnapsToDevicePixels �� Orientation�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      5
converters&clr-namespace:MahApps.Metro.Converters  5      1Controls$clr-namespace:MahApps.Metro.Controls  5      %�  &	        &	      &	 &     &	 9     7  (�� {    5+      *, +)7  7  7  7   '  D	       5      6-     5      6.     5      6.   �� 5
      "  ��6%   .�5   
   ��  � �� 6%   ��  �� "  $ FindAncestor=�$ HeaderFontSize��� 5   
    � �� 6'   ��  �� "  $ FindAncestor=�$ HeaderFontColor��� 5   
    � �� 5      �� 5      .R�5      �� ��6    �� ��61   ��  �� $ LabelL�$ 
Foreground�5      5      �� 5   
    � �� 6'   ��  �� "  $ FindAncestor=�$ HeaderFontFamily��� 5   
    � ��$ Light"�6'   �� 5   
    � ��$
 Left=�60   �� 5   
    � ��$
 Left=�67   �� 5   
    � ��$	 Top=�6.   �� 5   
    � ��$	 Top=�65    5   
   �� 5       6   ��  �� "  $ DynamicGroupHeader�		 False6�   .a�5      ��  � ��$ 	10,0,0,20q�6+   5      �� 5       6   ��  �� "  $ DynamicGroupHeader�	 True6�   .a�5      ��  � �� 5       `� 5!      8
   .1�5"      ��  6&   �� *Self+�� 5#      $ ContentHorizontalOffset�6b    6&   �� $ ScrollViewerL�6b   5$      5&      5'      5)      �� 5+      "  6    5,   
   ���� 5-      transparentListBoxItemStyle "  ��68    5.      ���� 5/      $ Transparent��6X   (��       6&   * SystemColors.HighlightBrushKey+)6X   50      .�51      ��  � ��$ 0q�6,   �� 52       � ��$ 0q�6+   53      54      .R�56   
   ]� ��. 
������ .+�57      �� $ header��59      8��  $	��Top=�5:      ��58      �� *Header+5:      �� 5;      $ items��5<      $ Single=�5=      $��0q�5>      
������ 
������ $	��Top=�5A      $
 TrueY�5B      8�� ��5;      �� *Tiles+��5E      �� 5F      .R�5G      � ��5H      `� 5I   "   8
  .1�5J   &   �� $ ItemBox�6.    6=   �� "  $ FindAncestor=��� 5K   &   $ ActualHeight�6.    6B   �� "  $ FindAncestor=��� 5L   &   $ ActualHeight�6.   $ header��6B   5M   #   ��5O      `� 5P   "   8
  .1�5Q   &   �� $ ItemBox�6.    6=   �� "  $ FindAncestor=��� 5R   &   $ ActualHeight�6.   $ items��6B   �� 5S   &    6.   �� " ��$ FindAncestor=��� 5T   &   $ Items.Count�6.   $ items��6A   5U   #   5W      5X      5[      5\      5]      �� 5_      "   6   .�5`   
   ��  � �� 5a      �� 5b      " ��6"    5c      ���� 5d      '��       "  ��6    .�5e      ��  � ��$ Hidden=�6N   �� 5f       � ��$ Hidden=�6L   5g      5h      .R�5j      �� $ PART_ScrollViewer��6#   #��z��6>   #��z��6h    . .��5k      �� $��0q�6)   # z 64   5l      5m      5o      �� 5p   
    � �� 5q      �� 5r      .R�5s      �� $ 
Horizontal=�6!   5t      5v      5w      5x      