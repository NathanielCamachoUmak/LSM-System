   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5      H DLSM_prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:LSM_prototype.CoreLSM_prototype.Core 5      K*clr-namespace:LSM_prototype.MVVM.ViewModelLSM_prototype.MVVM.ViewModel 5      ,   &LSM_prototype.MVVM.View.Analytics1View   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Clc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      2local clr-namespace:LSM_prototype.Core     5      @	viewmodel*clr-namespace:LSM_prototype.MVVM.ViewModel     5      -   5          >�Name$
  Root��5
       � 	Resources 5      &   LSM_prototype.Core.RectConverter   RectConverter-  'LSM_prototype.Core.ColumnWidthConverter  ColumnWidthConverter��%:   &	        &	        5   
   6     5   
   6%   5      .��5      �� $
��auto��6   
������� �� CornerRadius$ 25��5       �� Padding$ 15q�5      ._�5   
   � $	��500��6   $
��1000��6   .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      $��1* �6'   �� 5      $��5 �6'   �� 5      $��1* �6'    � RowDefinitions 5      �� 5      $
��auto �6$   �� 5      $
��auto �6$   �� 5       $��* �6$   �� 5!      $
��auto �6$   .+�5$      �� $��ORDERS THIS WEEK��6   $��0��64   $��0��6A   $��Center=�5%      $��Center=�5&      $��30(�5'      $
��Bold"�6*   �� 5)       �� Orientation$ 
Horizontal=�6   $��Center=�66   $��1��5*      $��0��6*   .+�5+      �� $��Total Orders: ��6    $��0,5,0,0q�66   $��20(�5,       $
��Bold"�6.   �� 5-      $��Bottom=�5.       $��20(�5/       $
��Bold"�6.   ��5-       �� *OrdersList.Count+5/   .   50      � 53      $��2��6   $��0��6$   .+�54         LiveCharts.Wpf.PieChart  -   5          Name$ MyPieChart1��54   "   &    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�56   "   $��20(�6:   $
��Bold"�6H   	   Series	 55   "   �� *	PieChart1+56   H   57      �� 59      
���   �$��1��60   $��4��6@   $��0,-15,0,-15q�6Q   �� 5;      $��REVENUE THIS WEEK��6   $��0��65   $��2��6B   $��Center=�5<      $��Center=�5=      $��30(�5>      $
��Bold"�6*   �� 5@      $ 
Horizontal=�6   $��Center=�66   $��1��6S   $��2��6`   .+�5A      �� $��Total Revenue: ��6    $��0,5,0,0q�67   $��20(�5B   $   $
��Bold"�62   �� 5C      $��Bottom=�5D   $   $��20(�5E   $   $
��Bold"�62   ��5C       �� *RevenueCollection+5E   2   5F      � 5I      $��2��6   $��2��6$   .+�5J        -   5        $ MyPieChart2��5J   "   $ Bottom=�5L   &   $��20(�6>   $
��Bold"�6L   	 5K   &   �� *	PieChart2+5L   L   5M      �� 5O      -   5        
 ��Name$
 ClippedButton2��5O   4   $��-15,0,-15,-15q�6   $��Bottom=�5P      $��3��68   $��0��6E   $��3��6U    J� Clip 5Q      �� 5R       �� RadiusX$ 25Q�6,    �� RadiusY$ 25Q�69    �� Rect 5S      `� 5T   "    `� 	Converter# [  6/   .1�5U   &   ��  �� ElementName$ ClippedButton2��6.    �� Path$ ActualWidth�6K   �� 5V   &   $ ClippedButton2��6.   $ ActualHeight�6K   5W   #   5Y      .+�5]      �� $��PRINT��6b   $��30(�6r   $��Black"�6�   $��50��5^   !   $��0q�6-   ��5]      �� $
 Root��$ DataContext.ExportCommand�5^   -   5_      5`      5a      5b      5c      