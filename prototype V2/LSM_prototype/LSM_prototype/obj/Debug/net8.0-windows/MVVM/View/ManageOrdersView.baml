   M S B A M L   `   `   `  ����H  DLSM_prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:LSM_prototype.CoreLSM_prototype.Core  5      K*clr-namespace:LSM_prototype.MVVM.ViewModelLSM_prototype.MVVM.ViewModel  5      .    (LSM_prototype.MVVM.View.ManageOrdersView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:LSM_prototype.Core     5      @	viewmodel*clr-namespace:LSM_prototype.MVVM.ViewModel     5      -   5          >�Name$
  Root��5
       � 	Resources 5      &    LSM_prototype.Core.RectConverter   RectConverter-   'LSM_prototype.Core.ColumnWidthConverter  ColumnWidthConverter��%:   &	        &	        5   
   6     5   
   6%   5      .��5      � $	��500��6   $
��1000��6   .+�5   
   �� -   5         ��Name$ ClippedBorder��5      $
��auto��5      
�������$��0,5,0,5q�5       �� CornerRadius$ 25��5       J� Clip 5      �� 5       �� RadiusX$ 25Q�6$    �� RadiusY$ 25Q�61    �� Rect 5      `� 5       `� 	Converter# [  6'   .1�5      �� 	 �� ElementName$	 ClippedBorder��6&   
 �� Path$
 ActualWidth�6B   �� 5      $	 ClippedBorder��6&   $
 ActualHeight�6B   5       5"      ._�5&      &   System.Windows.Controls.DataGrid     CanUserAddRows$ False��6      CanUserDeleteRows$ False��5'         AutoGenerateColumns$ False��5*         CanUserResizeColumns$ False��5+         CanUserSortColumns$
 True��5,      "   VerticalScrollBarVisibility$ Visible=�5-      $   HorizontalScrollBarVisibility$ Disabled=�5.         RowHeaderWidth$ 0Q�5/       %� 
ScrollUnit$ Pixel=�50      $��0q�51       �� SelectedItem 5(      �� *SelectedItem+��5)      �� *SharedOrders+ 52       �� Color��%R   (��       53   &   * SystemColors.HighlightBrushKey+)�� 6   $ White��6X   54         Columns 56      4  .System.Windows.Controls.DataGridTemplateColumn  58      ,  &System.Windows.Controls.DataGridColumn   Header$ All Job Orders��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6E      SortMemberPath$ OrderID��6O      HeaderStyle 59      �� 5:       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6$   .�5;   "   ��  �� Property � �� �� Value$ 24(�6=   �� 5<   "    � ��$ SemiBold"�6?   �� 5=   "    � ��$ 
30,10,0,10q�6<   5>         CellTemplate 5@      �� 5A      .R�5B   "   �� 
���   �$��20(�6?   $��SemiBold"�6M   $��Center=�5C   ,   $��20,2.5,0,2.5q�6G   ��6]   �� *	OrderID+5D      5F        5I      $ 
Start date��6-   $
 auto 6A   $ 	StartDate��6N    5J      �� 5K      "  6$   .�5L   "   ��  � ��$ 24(�6=   �� 5M   "    � ��$ SemiBold"�6?   �� 5N   "    � ��$ 	30,0,30,0q�6<   5O       5Q      �� 5R      .R�5S   "   �� 
���   �$��5q�6Z   $��Center=�5T   ,   $��Center=�6G   ��5S   ,   �� *	StartDate+5T   G   5U      5W        5Z      $  ��6-   $
 auto 67    5[      �� 5\      "  6$   .�5]   "   ��  � ��$ 24(�6=   �� 5^   "    � ��$ SemiBold"�6?   �� 5_   "    � ��$ 	30,0,30,0q�6<   5`       5b      �� 5c      .R�5d   "   �� $��
Edit Order��6)   $��Center=�6>   $��5q�6Y   ��5e   )   �� $
	 Root��$"
 DataContext.OpenOrderCommand�5f      5h      5k      5l      5m      5n      