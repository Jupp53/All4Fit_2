   M S B A M L   `   `   `  ����B  >All4Fit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:All4FitAll4Fit  5   	       All4Fit.UserHome   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   !localclr-namespace:All4Fit  5   	     � Title$  UserHome��5	   	   $	��800��6   $	��450��6'      MaterialDesignBody#���   5
   	    � AllowsTransparency .  � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   ��5   	   Z� .��5      #  MaterialDesignThemes.Wpf.Card     UniformCornerRadius$ 15Q�6     MaterialDesignPaper#���  63   $��25q�6f   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth4=�6r   .��5   
   )  #MaterialDesignThemes.Wpf.DialogHost  -   5          Name$ 
DialogHost��5   =      CloseOnClickAway . .��5      �� $��Hidden=�6   .��5      �� $
��1136��6   .+�5      '  !MaterialDesignThemes.Wpf.PopupBox  $��Right=�6.   $��	0 20 20 0q�6J      PlacementMode$ BottomAndAlignRightEdges=�6]   	   	StaysOpen	 .  $��25��6�   
   PopupContent.
 5      �� .+�5      �� -   5         ��Name$ 
btn_Logout��5   %   $��0 8 0 0q�69   $��Wyloguj��6J   6\   �� 5      -   5        $ btn_Exit��5   %   $��Zamknij aplikację��67   6S   5      5      �� 5      $��
0 -150 0 0q�6   $��Image/logo.png��60   $	��300��6H   �� 5      -   5         ��Name$ tbMenu��5       $��0 0 0 5q�60   $��Center=�6A   $��28(�6^   $
��Bold"�6l   $
��MENU��6~   �� 5      $��0,20,0,0q�6!   .+�5      �� $��Image/BMIbanner.png��6    �� 5      -   5        $ bmiBtn��5   3   $��0,-2,0,0q�6!    J� IsHitTestVisible .   ! MaterialDesignFlatMidBgButton#��[ 6\   $ Depth0=�6�   $��53��6�   $	��405��6�   $��25(�6�   $	��BMI��6�   
������ 
����������#008037$
��Left=�6;  5      �� 5      $��0,20,0,0q�6!   .+�5       �� $��Image/banner6.png��6    �� 5!      -   5        $ 
treningBtn��5!   !   #��[ 65   $ Depth0=�6l   $��53��6�   $	��400��6�   $��25(�6�   $��Trening��6�   
����������#008037$
��Left=�6�   60  5"      �� 5#      $��0,20,0,0q�6!   .+�5$      �� $��Image/banner2.jpg��6    �� 5%      -   5        $ dietBtn��5%   !   #��[ 62   $ Depth0=�6i   $��53��6�   $	��400��6�   $��25(�6�   $��Dieta��6�   
����������#008037$
��Left=�6�   6'  5&      5'      5(      5)      5*      5+      