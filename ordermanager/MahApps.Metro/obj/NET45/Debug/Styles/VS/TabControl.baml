   M S B A M L   `   `   `  ����\  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   Key �� Color  StandardTabControl �� 
TargetType �� 	Resources �� Property �� Value �� 
TargetType J� SnapsToDevicePixels	 � ColumnDefinitions
 �� Path F�Name �� Stretch  CloseButtonStroke �� StrokeEndLineCap �� StrokeStartLineCap �� Triggers ^� 
Conditions �� Binding �� RelativeSource �� Mode �� AncestorType �� Value P� Property P� Value $  CloseButtonBackgroundHighlighted �� 
TargetName   CloseButtonStrokeHighlighted   CloseButtonBackgroundPressed  BorderBrushSelected  BackgroundSelected-    'System.Windows.Input.KeyboardNavigation    TabNavigation � RowDefinitions K� ZIndex K� IsItemsHost    TabIndex  
Background 	 ClosableTabItemTemplate � 	Resources 
 
Foreground  WorkspacesTemplate$ �� IsSynchronizedWithCurrentItem�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      5        5      �� 5   
   $K Epack://application:,,,/MahApps.Metro;component/Styles/VS/Colours.xaml?�6   �� 5   
   �� 5
      BackgroundHighlighted  $ #54545C��6<   �� 5      BorderBrushHighlighted  $ #6A6A75��6=   �� 5      BackgroundSelected  $ 	#CCcb0000��69   �� 5      BorderBrushSelected  $ #1C97EA��6:   �� 5      BackgroundNormal  $ #3F3F46��67   �� 5      BorderBrushNormal  $ #54545C��68   �� 5      $ CloseButtonBackgroundHighlighted  $ #39ADFB��6G   �� 5       CloseButtonBackgroundPressed  $ #084E7D��6C   �� 5      CloseButtonStroke  $ 	#AAFFFFFF��68   �� 5       CloseButtonStrokeHighlighted  $ #FFFFFF��6C   5      %  &	       &		 �
    7 
 &	 �    7 	 �� 5      " ��6'    5   
   ���� 5      '��       " ��6   .�5      ��  �@�FocusVisualStyle 65   Z� �� 5       � ��
 ���� 6/   �� 5        � �� 5!      �� 5"      " ��6*   .R�5#      � $��30��6#   #��z��5$   #    . 	 5&   "   �� 5'   &   $
��Auto �67   �� 5(   &   $��25 �67   .+�5*   "   �� $��0��63   $��	10,0,10,0q�5+   3   $��Center=�5,   3   $��Center=�5-   3   $��Header��5.   3   �� 5/   "   $��1��6)   $��15��50   )   $��15��51   )   $��Center=�52   )   $��Center=�53   )   $��Right=�55   )   ��54   )   �� $
 CloseCommand���56   &   �� 57   *   " ��60   .�58   .   ��  � ��
 ���� 6K   �� 59   .    �@�Cursor$
 Hand��6G   �� 5:   .    � |� .  6J   �� 5;   .    � �� 5<   2   �� 5=   6   " ��6F   .R�5>   :   � #��z��6?   .+�5?   >   F� $ 
ButtonPath��6C   $��2q�5@   C   $��Center=�5A   C   $��Center=�5B   C   +����        �@B @B �    @B �@B     	$ Uniform=�5D   C   #���  5E   C   $
 Flat=�5F   C   $
 Flat=�5G   C   $��2��5H   C   5I   ;    5J   :   ^� 5K   >    5L   B   �� 5M   F    5N   I   �� *
IsSelected+ �� $ FindAncestor=�$ TabItemL�	 false5O   I   �� 5P   F    5Q   I   �� *IsMouseOver+ �� $ FindAncestor=�$ TabItemL�	 false5R   I   0�5T   B   �� 5U   F    � y�$ Hidden=�6c   5W   ?   P� 5X   >    � {� .  . �5Y   B   ��  � y�$ Hidden=�6_   5Z   ?   P� 5[   >    �@J�IsMouseOver . . �5\   B   �� # �  5]   I    � ���� 5^   B   $ 
ButtonPath��6I   # �  5_   I    � ��5`   ?   P� 5a   >    � �� . . �5b   B   �� # �  5c   I    � ���� 5d   B   $ 
ButtonPath��6I   # �  5e   I    � ���� 5f   B   $ 
ButtonPath��6I    � ��$ 2.5,2.5,1.5,1.5q�5g   I   5h   ?   5j   7   5l   /   5m   +   5o   #   5p       5q      P� 5r   "    �@��
IsSelected .  . �5s   &   ��  � ��
 ���� 6C   5t   #   P� 5u   "    �@J�IsMouseOver . . �5v   &   �� # �  6C    � ��5w   #   P� 5x   "    �@��
IsSelected . . �5y   &   �� # �  6C    � ��5z   #   5|      5~      5      5�      .�5�   
   ��  � �� 5�      �� 5�      " ��6"   .R�5�      � $ Local=�6    5�      �� 5�      $
��Auto �6,   �� 5�      $��* �6,   .+�5�      �� 
������ #���  5�   !   $��0,0,0,3q�5�   !   ._�5�      �� $��HeaderPanel��6'   $��0,0,4,-1q�5�   '   $ 1��5�   '    . $ 1��5�   '   5�      �� 5�      $��1��6!   #���  6.   �� 5�      $��PART_SelectedContentHost��6+   $��1��5�   +   $��SelectedContent��5�   +   5�      5�      5�      5�      �� 5�      .R�5�   
   ]� .+�5�      �� $��Center=�6   ��6:   �� *DisplayName+ 5�      ���� 5�      '��       " ��6   .�5�      ��  � ��8   67   �� 5�       � ��$ 20(�65   5�      5�      5�      5�      5�      �� 5�      .R�5�   
   �� $��0q�6   $
 TrueY�5�      8��  #���  5�      ��5�      �� 5�      5�      5�      