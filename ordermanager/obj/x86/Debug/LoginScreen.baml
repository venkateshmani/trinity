   M S B A M L   `   `   `  ����J  FMahApps.Metro, Version=0.0.0.999, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    ordermanager   ordermanager.LoginScreen   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	     � Icon$,  &/Resources/Icons/OrderManagerIcon1.ico��5   	    � Title$ Login��5   	   $��541.096��6   $��375.602��6(   ���	#FF353432 � 
ResizeMode$ NoResize=�6O    � WindowStyle$
 None=�6e    � WindowStartupLocation$ CenterScreen=�6x   (   "MahApps.Metro.Controls.MetroWindow   ShowCloseButton .     ShowTitleBar .   � 	Resources 5       �� MergedDictionaries	 �� Source   DescriptionHeaderStyle
 �� 
TargetType �� Property �� Value�� 5   
    5	      �� 5
      $H	 Bpack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml?�6%   �� 5      $F	 @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I	 Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e	 _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $O	 Ipack://application:,,,/MahApps.Metro;component/Metro.Resources/Icons.xaml?�6%   �� 5      $M	 Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R	 Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   �� 5      $K	 Epack://application:,,,/MahApps.Metro;component/Styles/FlatSlider.xaml?�6%   %z   &	        �� 5      "
 ��63   .�5      ��  � ��$ 16(�6-   �� 5       � ��$ Center=�68   5      5         	GlowBrush 5      �� 5   
    �� Color$ 	#FF353432��6   .��5      �� ���	#FF353432._�5      �  � RowDefinitions 5    
   �� 5!      $
��Auto �6   �� 5"      $��* �6   .+�5$   
   � ���	#FF353432$��0��6&    5%      �� 5&      $
��Auto �6    �� 5'      $
��Auto �6    �� 5(      $
��Auto �6    �� 5)      $
��Auto �6    �� 5*      $
��Auto �6    �� 5+      $
��Auto �6    .+�5-      ��  �� Orientation$ 
Horizontal=�6   $��
10,10,10,0q�62   .+�5.      ��   ContentFontFamily#���  6     HeaderFontSize#��[ 6M   $��Order Manager��6x   
�������6�   5/      �� 50      -   5         ��Name$ 
versionTxt��50      $��
15,0,10,10q�6,   $��1��6@   #���  6M   $��10(�6~   $��	Vxx.xx.xx��6�   
�������6�   �� 51      $��15,15,10,10q�6   $��2��6-   #���  6:   $��10(�6k   $0��*Licensed to TRINIITY COLOUR INDIA PVT LTD.��6y   
�������6�   �� 52      $��
15,15,10,5q�6   $��3��6,   #���  69   $��10(�6j   $6��0© 2014 Venkatesh Mani Selvaraj & Veera P Bolla ��6x   
�������6�   �� 53      $��
15,0,10,10q�6   $��4��6,   #���  69   $��10(�6j   $F��@    For any queries feel free to write to vmselvaraj@outlook.com��6x   
�������6�   55      � 57      ���	#FF353432$��Center=�6)   $��Bottom=�6F   $��1��6a   $��0,0,0,15q�6n    58      �� 59      $��75 �6    6.   �� 5:      $
��Auto �6    60   �� 5;      $
��Auto �6    60   .+�5=      �� $��0��6   ._�5>      �� -   5        $ tbCompanyName��5>      $��0q�63   #���  6>   $��30(�6o   $��	Log in...��6}   
�������$��Bottom=�6�   6�   5?      �� 5@      $��1��6   $ Vertical=�6&   $��0,10,0,0q�6=   $��Right=�6O   .+�5A      �� -   5         ��Name$ 
tbUserName��5A      $��Center=�6.   $��Center=�6I   $��Center=�6f   $��40��6�   $	��320��6�   
�������  NormalFontSize#��[ 6�   *   $MahApps.Metro.Controls.TextboxHelper   	Watermark$ 	User Name��5B       � ToolTip$ 	User name��68   G� 5C      -   5         G�Name$ 
tbPassword��5C      $��Center=�62   $��Center=�6M   $��Center=�6o   $��40��6�   $	��320��6�   $��0,10,0,0q�6�   
�������#��[ 6�   $ Password��5D      $ Password��67   6J   5E      �� 5F      $ 
Horizontal=�6   $��Right=�62   $��	0,25,0,10q�6O   $��2��6b   .+�5G      �� -   5         ��Name$ btnLogin��5G      $��Login��6+   $	��120��6<   $��30��6H   
�������#��[ 6g   $ Login��5H      $��Right=�6   ��5I      �� 5J        AccentColor# [ 6*   5L      �� 5M      -   5        $ btnExit��5M      $��Close��6*   $��5,0,0,0q�6;   $��80��6L   $��30��6W   
�������$ Close��5N      $��Right=�6   #��[ 68   ��5O      �� 5P        ValidationColor5# [ 6*   5R      5S      5T      5U      5V      5W      