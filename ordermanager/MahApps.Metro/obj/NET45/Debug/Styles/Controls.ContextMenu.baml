   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   	Checkmark  
RightArrow  DisabledMenuItemBrush  �� Color �� 
TargetType  MetroSeparator �� Property  	TextBrush �� Value �� 
TargetType �� 
ResourceId ��Name �� RadiusX �� RadiusY	 J� Opacity
 � ColumnDefinitions z� SharedSizeGroup ��Name J� SnapsToDevicePixels ��Name m� InputGestureText ��Name F�Name  BlackColorBrush �� Triggers P� Property P� Value �� 
TargetName  HighlightBrush m� Icon �Name � AllowsTransparency � HorizontalOffset � VerticalOffset �� RelativeSource ��Name �� TypeInTargetAssembly-    'System.Windows.Input.KeyboardNavigation    DirectionalNavigation � IsSharedSizeScope     TabNavigation  MetroContextMenu  ControlBackgroundBrush! �� CornerRadius" J� Effect3  -System.Windows.Media.Effects.DropShadowEffect#   ShadowDepth$   Opacity%   
BlurRadius& � Triggers' :� RoutedEvent( g� Duration	) W� To* �� TargetProperty+ �� 
TargetName 	 WhiteColorBrush 
 MetroMenuItem, �� AncestorType  ContentFontFamily  ContentFontSize- �� Triggers  StandardMenu. �� BasedOn�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %�&  &	        &	 P     &	 }     &	 �     (�� �    5      $ SubmenuItemTemplateKey��)7   (�� �    5<   ,   $ TopLevelItemTemplateKey��)&	 �    (�� �    5�      $ SubmenuContent��)(�� �    5�      $ TopLevelHeaderTemplateKey��)7   (�� �    5�      $ SubmenuHeaderTemplateKey��)7   7  &	
 �     &	 0%    7o��5      <8M 0,5.1 L 1.7,5.2 L 3.4,7.1 L 8,0.4 L 9.2,0 L 3.3,10.8 Z�6[   �5      M 0,0 L 4,3.5 L 0,7 Z�69   �� 5      $  	#FF9A9A9A��64   �� 5
      " ��6   .�5   
   ��  � ��# �  6'   �� 5   
    � �� .  6&   �� 5   
    � ��$ 6,2,6,2q�6#   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� #��z��6   #��z��5      #��z��5      $��1��5      5      5      5      �� 5      " m�6X   .R�5   
   � .+�5      �� $ Bg��6   $ 0��6$   $ 0��60   $		 .65Q�6<   #��z��6J   #��z��6n   #��z��6�   � 5      #��z��6   
 5      �� 5      $
��Auto �6'   $ MenuItemIconColumnGroup��64   �� 5       $��2 �6'   �� 5!      $��* �6'   �� 5"      $ MenuItemIGTColumnGroup��6'   $
��Auto �6P   �� 5#      $��17 �6'   .+�5%      �� $
 Icon��6#   $
��Icon��61   $��1q�6F   # z 6Q   $��Center=�6�   �� 5&      $ 	Presenter��6#   ��. #��z��6Q   #��z��6t   $��2��6�   �� 5'      $ InputGestureText��6   $��3��66   $��Right=�6F   $��5,0,0,0q�6]   #��z 6n   �� 5(      $ 
GlyphPanel��6   $��22��6-   $��1q�69   $y�	Collapsed=�6D   $��22��6[   ._�5)      F� $ Glyph��6   8��  #���  6L   $��LeftToRight=�6u   $��11��6�   $��9��6�   5*      5+      5,       5-   
   P� 5.       �@m�IsHighlighted . . �5/      �� $ Bg��6)   # �  69    � ���� 50       � �� �	#FFFFFFFF6/   51      P� 52       �@m�	IsChecked . . �53      �� $ 
GlyphPanel��6/    � y�$ Visible=�6G   �� 54      $
 Icon��6/    � y�$ 	Collapsed=�6A   55      P� 56       � {� .  . �57      �� # �  6/    � ��58      5:      �� 5<      " m�6   .R�5=   
   � .+�5>      �� $ Bg��6   $ 0��6$   $ 0��60   $		 .65Q�6<   #��z��6J   #��z��6n   #��z��6�   � 5?      #��z��6   
 5@      �� 5A      $
��Auto �6'   $ MenuItemIconColumnGroup��64   �� 5B      $��2 �6'   �� 5C      $��* �6'   �� 5D      $��17 �6'   .+�5G      �� $ IconPresenter��6#   #��z 6:   $��1q�6[   $	 .7Q�6f   $��Center=�6s   �� 5H      $ 	Presenter��6#   ��. #��z��6Q   #��z��6t   $��2��6�   � 5I      $ 
PART_Popup��6    . |�.  $ -2��6X   #��� -�6�   $��Right=�6  $ -3��6'  ��6n   �� *IsSubmenuOpen+ �� *TemplatedParent+.��5K      �� $ SubMenuBorder��6%   ��.  ��6N   C� *�� $ SubmenuContent��" �+.��5L      �� ��. ��6?   C� *�� $ MenuScrollViewer��" �+.��5M      �� $ Cycle=�6-    . $��2q�6|   # z 6�   $  Cycle=�6�   5N      5O      5Q      5R      5S       5T   
   P� 5U       �@m�IsHighlighted . . �5V      �� $ Bg��6)   # �  69    � ���� 5W       � �� �	#FFFFFFFF6/   5X      P� 5Y       � {� .  . �5Z      �� # �  6/    � ��5[      5]      �� 5a      " ��6   .�5b   
   ��  � ��# �  6'   �� 5c   
    � ��$ 1q�6,   �� 5d   
    � ��# �  6(   �� 5e   
    � ��$ 0,4,0,4q�6$   �� 5f   
    � �� 5g      �� 5h      " ��6"   .R�5i      �� $
 Menu��5j      $��5,5,5,5q�5i      $	 0Q�5k      #��z��5l      #��z��5m      #��z��5n      $! 0��5o      " 5p        5q      $# 1Q�6/   $	$ 0.6Q�6?   $% 5Q�6M   & 5s      :� 5t      $' Rectangle.Loaded��6+   .[�5u   "   �� .��5v   &   �� .��5w   *   W� $( 0:0:0.2D�6:   $) 1X�6M   $* Opacity�6T   $
+ Menu��6x   5x   '   5y   #   5z      ._�5|      � .+�5}      �� #��z��6-   5~      5      5�      5�      5�      �� 5�      " ��6O   .R�5�   
   �� #���  6   $��1q�6A   #��� 	 6U   ._�5�      �� 5�       5�   
   P� 5�       � {� .  . �5�      �� # �  6/    � ��5�      P� 5�       � {� . . �5�      �� # �  6/    � ��5�      5�      �� 5�      " m�6Z   .R�5�   
   �  . .+�5�      �� $ Bg��6   #��z��6$   $
	 0.65Q�6H   $ 2��6W   $ 2��6c   #��z��6o   $��1��6�   �� 5�      $ InnerBorder��6   $��1q�6-   $ 2��68   $ 2��6D   
������ $��1��6e   � 5�      
 5�      �� 5�      $��2 �6'   �� 5�      $��* �6'   �� 5�      $��37��6'   .+�5�      �� $ 
GlyphPanel��6   $��22��6-   $��1q�69   $y�	Collapsed=�6D   $��22��6[   ._�5�      F� $ Glyph��6   8��  #���  6L   $��LeftToRight=�6u   $��11��6�   $��9��6�   5�      �� 5�      $��2��6#   $��Header��64   #��z��6K   ��. # z 6�   �� 5�      $��4��6   #��z��6,   #��z 6O   $y�	Collapsed=�6y   5�      � 5�      $ 
PART_Popup��6    . |�.  $ -2��6T   #��� -�6�   $��Bottom=�6  $ -3��6$  ��6j   �� *IsSubmenuOpen+ �� *TemplatedParent+.��5�      �� $ SubMenuBorder��6!   ��.  ��6J   C� *�� $ SubmenuContent��+.��5�      �� ��. ��6;   C� *�� $ MenuScrollViewer��" �+.��5�      �� $ Cycle=�6)    . $��2q�6x   # z 6�   $  Cycle=�6�   5�      5�      5�      5�       5�   
   P� 5�      " �@m�IsSuspendingPopupAnimation . . �5�      �� $ 
PART_Popup��63    � ��$
 None=�6K   5�      P� 5�       �@m�IsHighlighted . . �5�      �� $ InnerBorder��6+    � �� �	#D1DBF4FF6D   5�      P� 5�       �@m�IsHighlighted . . �5�      �� $ Bg��6)   # �  69    � ���� 5�       � �� �	#FFFFFFFF6/   5�      P� 5�       � {� .  . �5�      �� # �  6/    � ���� 5�      $ 
GlyphPanel��6/    � �� �#EEE9E96G   �� 5�      $ 
GlyphPanel��60    � �� �#DBD6D66H   �� 5�      $ Glyph��6)    � �� �#8485896<   5�      5�      �� 5�      " m�6Y   .R�5�   
   �  . .+�5�      �� $ Bg��6   #��z��6$   $
	 0.65Q�6H   $ 2��6W   $ 2��6c   #��z��6o   $��1��6�   �� 5�      $ InnerBorder��6   $��1q�6-   $ 2��68   $ 2��6D   
������ $��1��6e   � 5�      
 5�      �� 5�      $ MenuItemIconColumnGroup��6'   $
��Auto �6Q   �� 5�      $��2 �6'   �� 5�      $��* �6'   �� 5�      $��37 �6'   �� 5�      $ MenuItemIGTColumnGroup��6'   $
��Auto �6P   �� 5�      $��17 �6'   .+�5�      �� $
 Icon��6#   $
��Icon��61   $��1q�6F   # z 6Q   $��Center=�6�   �� 5�      $ 
GlyphPanel��6   $��22��6-   $��1q�69   $y�	Collapsed=�6D   $��22��6[   ._�5�      F� $ Glyph��6   8��  #���  6L   $��LeftToRight=�6u   $��11��6�   $��9��6�   5�      �� 5�      $��2��6#   $��Header��64   #��z��6K   ��. # z 6�   �� 5�      $��4��6   #��z��6,   #��z 6O   $y�	Collapsed=�6y   F� 5�      $��5��6   8�� #��z��6J   $��4,0,0,0q�6n   $��Center=�6   5�      � 5�      $ 
PART_Popup��6    . |�.  $ -2��6T   #��� -�6�   $��Right=�6  $ -3��6#  ��6j   �� *IsSubmenuOpen+ �� *TemplatedParent+.��5�      �� $ SubMenuBorder��6%   ��.  ��6N   C� *�� $ SubmenuContent��+.��5�      �� ��. ��6?   C� *�� $ MenuScrollViewer��" �+.��5�      �� $ Cycle=�6-    . $��2q�6|   # z 6�   $  Cycle=�6�   5�      5�      5�      5�       5�   
   P� 5�      " �@m�IsSuspendingPopupAnimation . . �5�      �� $ 
PART_Popup��63    � ��$
 None=�6K   5�      P� 5�       �@m�IsHighlighted . . �5�      �� $ InnerBorder��6+    � �� �	#D1DBF4FF6D   5�      P� 5�       6&   Z�  �@m�Icon. �5�      �� $
 Icon��6/    � y�$ 	Collapsed=�6A   5�      P� 5�       �@m�	IsChecked . . �5�      �� $ 
GlyphPanel��6/    � y�$ Visible=�6G   �� 5�      $
 Icon��6/    � y�$ 	Collapsed=�6A   5�      P� 5�       �@m�IsHighlighted . . �5�      �� $ Bg��6)   # �  69    � ���� 5�       � �� �	#FFFFFFFF6/   5�      P� 5�       � {� .  . �5�      �� # �  6/    � ���� 5�      $ 
GlyphPanel��6/    � �� �#EEE9E96G   �� 5�      $ 
GlyphPanel��60    � �� �#DBD6D66H   �� 5�      $ Glyph��6)    � �� �#8485896<   5�      5�      �� 5�      " m�6"   .�5�   
   ��  � �� 67   �� *HorizontalContentAlignment+ �� ", ���� 5�   
    � �� 65   �� *VerticalContentAlignment+ �� ", ���� 5�   
    � ��# �  6'   �� 5�   
    � ��# �  6'   �� 5�   
    � ��# �  6%   �� 5�   
    � ��# �  6'   �� 5�   
    � ��# �  6(   �� 5   
    � ��$ 0q�6,   �� 5  
    � ��$ 8,2,10,2q�6$   �� 5  
    � �� 6%   C� *�� $ SubmenuItemTemplateKey��+- 5  
   P� 5      �@m�Role$ TopLevelHeader=�6&   . �5     ��  � ��$ 7,2,8,3q�6,   �� 5      � �� 6-   C� *�� $ TopLevelHeaderTemplateKey��+5     P� 5      �@m�Role$ TopLevelItem=�6&   . �5	     ��  � ��$ 7,2,8,3q�6,   �� 5
      � �� 6-   C� *�� $ TopLevelItemTemplateKey��+5     P� 5      �@m�Role$ SubmenuHeader=�6&   . �5     ��  � ��$ 2,3,2,3q�6,   �� 5      � �� 6-   C� *�� $ SubmenuHeaderTemplateKey��+5     P� 5      �@m�Role$ SubmenuItem=�6&   . �5     ��  � ��$ 2,3,2,3q�6,   5     5     �� 5     " o�6   8.   .�5  
   ��  � ��# �  6'   �� 5  
    � ��# �  6'   5     5     