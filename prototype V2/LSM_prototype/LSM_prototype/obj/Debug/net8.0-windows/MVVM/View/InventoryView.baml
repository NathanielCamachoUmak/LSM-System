   M S B A M L   `   `   `  ����H  DLSM_prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:LSM_prototype.CoreLSM_prototype.Core  5      +    %LSM_prototype.MVVM.View.InventoryView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:LSM_prototype.Core     5      -   5          >�Name$
  Root��5       � 	Resources 5      &    LSM_prototype.Core.RectConverter   RectConverter��%   &	          5   
   6   5      .��5      � $	��500��6   $
��1000��6    � RowDefinitions 5   
   �� 5      -   5         �Name$
 row1��5   (   $��1* �6   6(   �� 5      -   5        $
 row2��5   (   $��9* �6   6(   .+�5   
   �� $��0��6   $
��auto��5      
�������$��0,5,0,5q�5       �� CornerRadius$ 25��5       �� Padding$ 	15,0,15,0q�5      ._�5      9� -   5        $��Add Item��5      $	��Top=�5      $��	10,5,10,5q�5       $��20(�5!      .��5#      �� 
�������.+�5$      �� $��Employee Information��6   $��15(�6=   $��3q�6K   .��5%      �  � ColumnDefinitions 5&      �� 5'   "   $��1* �63   �� 5(   "   $��1* �63   �� 5)   "   $��1* �63   .+�5,      � $��0��6#    5-   "   �� 5.   &   $��1* �67   �� 5/   &   $
��2.9* �67   .+�51   "   �� $��0��6,   $��Item Name: ��6<   $��14(�6O   $��5q�6]   �� 52   "   $��1��6*     CustomTextBoxStyle#��[ 6:   
 � Tag$ 	Item Name��6f   $��14(�6�   $��5q�6�   ��6v   �� *NewItemName+ �� UpdateSourceTrigger$ PropertyChanged=�6�   53      � 55      $��1��6#    56   "   �� 57   &   $��1* �67   �� 58   &   $
��2.9* �67   .+�5:   "   �� $��0��6,   $��Item Price: ��6<   $��14(�6P   $��5q�6^   �� 5;   "   $��1��6*   #��[ 6:   $ 
Item Price��6f   $��14(�6�   $��5q�6�   ��6w   �� *NewItemPrice+$ PropertyChanged=�6�   5<      � 5>      $��2��6#    5?   "   �� 5@   &   $��1* �67   �� 5A   &   $
��2.9* �67   .+�5C   "   �� $��0��6,   $��
Quantity: ��6<   $��14(�6N   $��5q�6\   �� 5D   "   $��1��6*   #��[ 6:   $ Item Quantity��6f   $��14(�6�   $��5q�6�   ��6z   �� *NewItemStock+$ PropertyChanged=�6�   5E      5G      5H      �� 5J      $��Add Item��6   ��60   �� *
AddCommand+5K      5L      5M      �� 5P   
   -   5        	 ��Name$	 ClippedBorder��5U      $��1��5P      $
��auto��5Q      
�������$��0,5,0,5q�5S      $ 25��5T      
 J� Clip
 5V      �� 5W       �� RadiusX$ 25Q�6$    �� RadiusY$ 25Q�61    �� Rect 5X      `� 5Y       `� 	Converter# [  6'   .1�5Z      ��  �� ElementName$ ClippedBorder��6&    �� Path$ ActualWidth�6B   �� 5[      $ ClippedBorder��6&   $ ActualHeight�6B   5\      5^      ._�5`      &   System.Windows.Controls.DataGrid     CanUserAddRows$ False��6      CanUserDeleteRows$ False��5a         AutoGenerateColumns$ False��5d         CanUserResizeColumns$ False��5e         CanUserSortColumns$
 True��5f      "   VerticalScrollBarVisibility$ Visible=�5g      $   HorizontalScrollBarVisibility$ Disabled=�5h         RowHeaderWidth$ 0Q�5i       %� 
ScrollUnit$ Pixel=�5j      $��0q�5k      ��5b      �� *Items+ �� SelectedItem 5c      �� *SelectedItem+ 5l       �� Color��%R   (��       5m   &   * SystemColors.HighlightBrushKey+)�� 6   $ White��6X   5n         Columns 5p      4  .System.Windows.Controls.DataGridTemplateColumn  5t      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Inventory��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6@      SortMemberPath$
 Name��6J       HeaderStyle  5u      �� 5v      ! �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"!  6$   .�5w   "   �� " �� Property" � ��# �� Value$# 24(�6=   �� 5x   "   " � ��$# SemiBold"�6?   �� 5y   "   " � ��$# 
30,10,0,10q�6<   5z      $   CellTemplate$ 5|      �� 5}      .R�5�   "   9� $	��Top=�6C   $��20,2.5,0,2.5q�6[   ��6+   �� *Name+.��5�   &   � 
�������$��10q�6>    5�   *   �� 5�   .   $��7* �6?   �� 5�   .   $��1* �6?   .+�5�   *   �� $��Employee Information��63   $��15(�6Q   $��3q�6_   $��0��6s   $��Center=�6�   .��5�   .   �  5�   2   �� 5�   6   $��1* �6G   �� 5�   6   $��1* �6G    5�   2   �� 5�   6   $��1* �6D   �� 5�   6   $��1* �6D   .+�5�   2   � $��0��67   $��0��6G   $��2��6T    5�   6   �� 5�   :   $��80 �6K   �� 5�   :   $��3* �6K   .+�5�   6   �� $��0��6@   $��
Item Name:��6P   $��14(�6b   $��5q�6p   % �� TextAlignment$% Right=�6{   �� 5�   6   $��1��6>   
�������#��[ 6e   $ 	Item Name��6�   $��14(�6�   $��5q�6�   ��6�   �� *Name+$ PropertyChanged=�6�   5�   3   � 5�   2   $��0��67   $��1��6G    5�   6   �� 5�   :   $��80 �6K   �� 5�   :   $��3* �6K   .+�5�   6   �� $��0��6@   $��Item Price:��6P   $��14(�6c   $��5q�6q   $% Right=�6|   �� 5�   6   $��1��6>   
�������#��[ 6e   $ 
Item Price��6�   $��14(�6�   $��5q�6�   ��6�   �� *Price+$ PropertyChanged=�6�   5�   3   � 5�   2   $��1��67   $��1��6G    5�   6   �� 5�   :   $��65 �6K   �� 5�   :   $��3* �6K   .+�5�   6   �� $��0��6@   $��	Quantity:��6P   $��14(�6a   $��5q�6o   $% Right=�6z   �� 5�   6   $��1��6>   
�������#��[ 6e   $ Item Quantity��6�   $��14(�6�   $��5q�6�   ��6�   �� *Stock+$ PropertyChanged=�6�   5�   3   5�   /   5�   +   �� 5�   *   $��1��65   $��Center=�6E   .+�5�   .   �� $��10q�65   $
��Bold"�5�   5   $��SAVE CHANGES��5�   5   
���������5�   5   �� $
 Root��$ DataContext.SaveCommand��� 5�   .   $��10q�65   $
��Bold"�5�   5   $��DELETE ITEM��5�   5   ���	#FFFF8D8D��5�   5   �� $
 Root��$ DataContext.DeleteCommand�5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      5�      