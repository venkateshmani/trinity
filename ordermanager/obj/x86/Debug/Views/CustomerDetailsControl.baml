   M S B A M L   `   `   `  ����J  FMahApps.Metro, Version=0.0.0.999, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5       ordermanager/   )ordermanager.Views.CustomerDetailsControl   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5        � WindowStartupLocation$  CenterScreen=�5      $	��400��6D   $	��525��6P    � 	Resources 5       �� MergedDictionaries �� Source   DescriptionHeaderStyle �� 
TargetType �� Property �� Value  HideHeaderStyle  ProductInfoStyle  ContentFontFamily�� 5	   
    5
      �� 5      $H Bpack://application:,,,/MahApps.Metro;component/Styles/Colours.xaml?�6%   �� 5      $F @pack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro;component/Styles/Controls.xaml?�6%   �� 5      $e _pack://application:,,,/MahApps.Metro;component/Styles/Controls.AnimatedSingleRowTabControl.xaml?�6%   �� 5      $O Ipack://application:,,,/MahApps.Metro;component/Metro.Resources/Icons.xaml?�6%   �� 5      $M Gpack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml?�6%   �� 5      $R Lpack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml?�6%   �� 5      $K Epack://application:,,,/MahApps.Metro;component/Styles/FlatSlider.xaml?�6%   %�  &	        &	 p     &	 �     7  �� 5      " ��63   .�5      ��  � ��$ 22(�6-   �� 5       � ��$ Center=�68   5      �� 5      " ��6,   .�5      ��  � y�$ 	Collapsed=�6/   5      �� 5      " ��6-   .�5      ��  � ��$ Center=�66   �� 5       � ��$ Center=�68   �� 5       � ��$ 5q�6+   �� 5       � ��$ 16(�6-   �� 5        � ��8   6/   5!      5"      .��5$      �� $��5q�6   .+�5%   
   �� $��Stretch=�6   $��Stretch=�6-   
�������._�5&      �� -   5         ��Name$ 	lblHeader��5&      $��Center=�6'   $��Center=�6B   $��Customer Details��6_   #��[  6{   5'      �� 5(   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6N   
�������*   $MahApps.Metro.Controls.TextboxHelper   	Watermark$ Customer Name��5)   !   	 � ToolTip$	 Customer name��6R   ��5*   !   �� *Name+�� 5+   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6N   
�������$ 
Alias Name��5,   !   $	 
Alias name��6O   ��5-   !   �� *Alias+�� 5.   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6L   
�������$ Street/Road #��5/   !   $&	  Customer address - Street/Road #��6R   ��50   !   �� *
Address1+�� 51   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6M   
�������$
 Area��52   !   $	 Customer address - Area��6I   ��53   !   �� *
Address2+�� 54   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6L   
�������$
 City��55   !   $	 	City name��6I   ��56   !   �� *City+�� 57   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6L   
�������$ State��58   !   $	 
State name��6J   ��59   !   �� *State+�� 5:   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6M   
�������$ Country��5;   !   $	 Country name��6L   ��5<   !   �� *	Country+�� 5=   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6M   
�������$ Telephone Work��5>   !   $	 Work telephone number��6S   ��5?   !   �� *	Telephone+�� 5@   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6M   
�������$ Telephone Fax��5A   !   $	 
Fax number��6R   ��5B   !   �� *Fax+�� 5C   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6M   
�������$ Mobile��5D   !   $	 Mobile number��6K   ��5E   !   �� *Mobile+�� 5F   
   $��Center=�6   $��Stretch=�6-   $��0,5,0,0q�6M   
�������$ Email��5G   !   $	 Customer email ID��6J   ��5H   !   �� *EmaiID+�� 5I   
   -   5        
 ��Name$
 	txtBoxTIN��5J   X   $��Center=�5I      $��Stretch=�6-   $��0,5,0,0q�6M   
�������$	 TIN��5J   !   $	 TIN #��6H   ��5K   !   �� *TIN+�� 5L   
   -   5        $
 	txtBoxCST��5M   V   $��Center=�5L      $��Stretch=�6-   $��0,5,0,0q�6M   
�������$	 CST��5M   !   $		 CST��6H   ��5N   !   �� *CST+�� 5O   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   .+�5P      �� -   5         ��Name$ btnSave��5P      $
��Save��6&   $��5,10,0,5q�66   $��75��6H   $��20��6S   
�������$	 Save Changes��5Q   )   $��Right=�6@   ��5R      �� 5S        AccentColor �� Color# [ 6&   5U      5V      5X      5Y      