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
��2.9* �67   .+�51   "   �� $��0��6,   $��Item Name: ��6<   $��14(�6O   $��5q�6]   �� 52   "   $��1��6*     CustomTextBoxStyle#��[ 6:   $��14(�54   *   $��5q�68   
 � Tag$ 	Item Name��6C   ��53   *   �� *newItem.Name+ �� UpdateSourceTrigger$ PropertyChanged=�54   C   55      � 57      $��1��6#    58   "   �� 59   &   $��1* �67   �� 5:   &   $
��2.9* �67   .+�5<   "   �� $��0��6,   $��Item Price: ��6<   $��14(�6P   $��5q�6^   �� 5=   "   $��1��6*   #��[ 6:   $��14(�5?   *   $��5q�68   $ 
Item Price��6C   ��5>   *   �� *newItem.PriceInput+$ PropertyChanged=�5?   C   5@      � 5B      $��2��6#    5C   "   �� 5D   &   $��1* �67   �� 5E   &   $
��2.9* �67   .+�5G   "   �� $��0��6,   $��
Quantity: ��6<   $��14(�6N   $��5q�6\   �� 5H   "   -   5        $��1��5H   *   #��[ 6:   $��14(�5J   *   $��5q�68   $ Item Quantity��6C   ��5I   *   �� *newItem.Stock+$ PropertyChanged=�5J   W   5K      5M      5N      �� 5P      $��Add Item��6   ��60   �� *
AddCommand+5Q      5R      5S      �� 5V   
   -   5        	 ��Name$	 ClippedBorder��5[      $��1��5V      $
��auto��5W      
�������$��0,5,0,5q�5Y      $ 25��5Z      
 J� Clip
 5\      �� 5]       �� RadiusX$ 25Q�6$    �� RadiusY$ 25Q�61    �� Rect 5^      `� 5_       `� 	Converter# [  6'   .1�5`      ��  �� ElementName$ ClippedBorder��6&    �� Path$ ActualWidth�6B   �� 5a      $ ClippedBorder��6&   $ ActualHeight�6B   5b      5d      ._�5f      &   System.Windows.Controls.DataGrid     CanUserAddRows$ False��6      CanUserDeleteRows$ False��5g         AutoGenerateColumns$ False��5j         CanUserResizeColumns$ False��5k         CanUserSortColumns$
 True��5l      "   VerticalScrollBarVisibility$ Visible=�5m      $   HorizontalScrollBarVisibility$ Disabled=�5n         RowHeaderWidth$ 0Q�5o       %� 
ScrollUnit$ Pixel=�5p      $��0q�5q      ��5h      �� *Items+ �� SelectedItem 5i      �� *SelectedItem+ 5r       �� Color��%R   (��       5s   &   * SystemColors.HighlightBrushKey+)�� 6   $ White��6X   5t         Columns 5v      4  .System.Windows.Controls.DataGridTemplateColumn  5z      ,  &System.Windows.Controls.DataGridColumn   Header$ Stock��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6<      SortMemberPath$
 Name��6G       HeaderStyle  5{      �� 5|      ! �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"!  6$   .�5}   "   �� " �� Property" � ��# �� Value$# 24(�6=   �� 5~   "   " � ��$# SemiBold"�6?   �� 5   "   " � ��$# 
30,10,0,10q�6<   5�      $   CellTemplate$ 5�      �� 5�      .R�5�   "   �� $��1��6*   #��[ 6:   $��14(�5�   *   $��5q�68   $ 	Item Name��6C   ��5�   *   �� *Name+$ PropertyChanged=�5�   C   5�      5�        5�      $ Price��6-   $ 1* 6<   $ Price��6G     5�      �� 5�      "!  6$   .�5�   "   �� " � ��$# 24(�6=   �� 5�   "   " � ��$# SemiBold"�6?   �� 5�   "   " � ��$# 
30,10,0,10q�6<   5�      $ 5�      �� 5�      .R�5�   "   �� $��1��6*   #��[ 6:   $��14(�5�   *   $��5q�68   $ 
Item Price��6C   ��5�   *   �� *
PriceInput+$ PropertyChanged=�5�   C   5�      5�        5�      $ Stock��6-   $ 1* 6<   $ Stock��6G     5�      �� 5�      "!  6$   .�5�   "   �� " � ��$# 24(�6=   �� 5�   "   " � ��$# SemiBold"�6?   �� 5�   "   " � ��$# 
30,10,0,10q�6<   5�      $ 5�      �� 5�      .R�5�   "   �� -   5        $��1��5�   *   #��[ 6:   $��14(�5�   *   $��5q�68   $ Item Quantity��6C   ��5�   *   �� *Stock+$ PropertyChanged=�5�   W   5�      5�        5�      $
 auto 6-   $
 Name��6:     5�      �� 5�      "!  6$   .�5�   "   �� " � ��$# 24(�6=   �� 5�   "   " � ��$# SemiBold"�6?   �� 5�   "   " � ��$# 
30,10,0,10q�6<   5�      $ 5�      �� 5�      .R�5�   "   �� $��1��6-   % �� Orientation$% 
Horizontal=�6=   $��Center=�6V   .+�5�   &   �� $��5,5,0,5q�6-   $
��Bold"�5�   -   $��SAVE CHANGES��5�   -   
���������5�   -   �� $
 Root��$ DataContext.SaveCommand��� 5�   &   $��5q�6-   $
��Bold"�5�   -   $��DELETE ITEM��5�   -   ���	#FFFF8D8D��5�   -   �� $
 Root��$ DataContext.DeleteCommand�5�   #   5�      5�      5�      5�      5�      5�      