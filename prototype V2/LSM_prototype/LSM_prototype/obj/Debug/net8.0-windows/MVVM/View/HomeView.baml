   M S B A M L   `   `   `  ����H  DLSM_prototype, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:LSM_prototype.MVVM.ViewLSM_prototype.MVVM.View  5      &     LSM_prototype.MVVM.View.HomeView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:LSM_prototype.MVVM.View     5        � 	Resources  5       #  CalendarCalendarDayButtonStyle1 �� 
TargetType:  4System.Windows.Controls.Primitives.CalendarDayButton �� Property �� Value �� 
TargetType'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   Transitions%  System.Windows.VisualTransition   GeneratedDuration   States   System.Windows.VisualState	   
Storyboard
 g� Duration �� 
TargetName	 W� To �� TargetProperty R� KeyTime R� Value  System.Windows.Visibility	 �� To ��Name J� Opacity �� RadiusX �� RadiusY ��Name F�Name J� RenderTransformOrigin �� Stretch J� IsHitTestVisible  CalendarCalendarItemStyle15  /System.Windows.Controls.Primitives.CalendarItem � 	Resources �Name  Key �� Color �� CornerRadius � Cursor � ColumnDefinitions  � RowDefinitions! ��Name  HeaderButtonTemplate" �� Orientation  PreviousButtonTemplate  NextButtonTemplate  DisabledColor# �� Triggers$ P� Property% P� Value& �� 
TargetName' �� Value( �� Binding) �� RelativeSource* �� AncestorType&   System.Windows.Controls.Calendar��%K$  &	        &	 �    7  7  7  7  7  �� 5	   
   "  68   .�5
      ��  � ��$ 5��6)   �� 5       � ��$ 5��6*   �� 5       � ��$ 10(�6)   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � �� 5      �� 5      "  6&   .R�5      � $��55��6   $��20��6*    5        5   "   CommonStates�� 5   &     5   *   $ 0:0:0.1D�6;   . 5   &     
Normal��62     5   &   	MouseOver��.	 5   *   �� .��5   .   W� $
 0D�6>   $ HighlightBackground��6K    16w   $ Opacity�6~   5   +   5   '     5   &   Pressed��.	 5   *   �� .��5    .   W� $
 0D�6>   $ HighlightBackground��6K    16w   $ Opacity�6~   5!   +   5"   '     5#   &   Disabled��.	 5$   *   �� .��5%   .   W� $
 0D�6>   $ HighlightBackground��6K    06w   $ Opacity�6~   W� 5&   .   $
 0D�6>   $ 
NormalText��6K    .356n   $ Opacity�6w   5'   +   5(   '   5)   #     5*   "   SelectionStates�� 5+   &     5,   *   $ 0D�6;   . 5.   &     
Unselected��62     5/   &   Selected��.	 50   *   �� .��51   .   W� $
 0D�6>   $ SelectedBackground��6K    16v   $ Opacity�6}   52   +   53   '   54   #     55   "   CalendarButtonFocusStates�� 56   &     57   *   $ 0D�6;   . 59   &     CalendarButtonFocused��.	 5:   *   �� .��5;   .   T� $
 0D�6L   $ DayButtonFocusVisual��6Y   $ 
Visibility�6�   ..�5<   2   i� $ 0��6I    5=   6    5>   :   Visible=�6N   5@   3   5A   /   5B   +   5C   '     5D   &   CalendarButtonUnfocused��.	 5E   *   �� .��5F   .   T� $
 0D�6L   $ DayButtonFocusVisual��6Y   $ 
Visibility�6�   ..�5G   2   i� $ 0��6I    5H   6    5I   :   	Collapsed=�6P   5K   3   5L   /   5M   +   5N   '   5O   #     5P   "   ActiveStates�� 5Q   &     5R   *   $ 0D�6;   . 5T   &     
Active��62     5U   &   Inactive��.	 5V   *   �� .��5X   .   �� $
 0D�6=   $ 
NormalText��6J    DarkGray6m   $6 0(TextElement.Foreground).(SolidColorBrush.Color)�6{   5Y   +   5Z   '   5[   #     5\   "   	DayStates�� 5]   &     5^   *   $ 0D�6;   . 5`   &     
RegularDay��62     5a   &   	Today��.	 5b   *   �� .��5d   .   W� $
 0D�6>   $ TodayBackground��6K    16s   $ Opacity�6z   �� 5e   .   $
 0D�6=   $ 
NormalText��6J   	 White6m   $6 0(TextElement.Foreground).(SolidColorBrush.Color)�6x   5f   +   5g   '   5h   #     5i   "   BlackoutDayStates�� 5j   &     5k   *   $ 0D�6;   . 5m   &     	NormalDay��62     5n   &   BlackoutDay��.	 5o   *   �� .��5p   .   W� $
 0D�6>   $ Blackout��6K    .26l   $ Opacity�6t   5q   +   5r   '   5s   #   .+�5v      �� $ TodayBackground��6(   ���	#FF585454$ 0Q�6R   $ 10��6^   $ 10��6k   $��2q�6x   �� 5x      $ SelectedBackground��6(   ���	#FF585454$ 0Q�6U   $ 10��6a   $ 10��6n   $��2q�6{   �� 5z      
������ #��z��6>   #��z��6j   �� 5|      $ HighlightBackground��6(   ���	#FF585454$ 0Q�6V   $ 10��6b   $ 10��6o   $��2q�6|   �� 5~      $ 
NormalText��6/   $
��bold"�6C   
�������#��z��6~   $��5,1,5,1q�6�   #��z��6�   F� 5      $ Blackout��6#   }�����5 �Z @�J� �Q2� �J� �̉� iV� �A�� �J� �W�� �J� ѷ�� #�� �>8� 7T�K�� 7T�� � ��� ћ�� 7T����G @7T��� �D� 	
���  ��$��Stretch=�6D  $��3q�6b  $ 0Q�6m  $ 0.5,0.5&�6y  $
 Fill=�6�  $��Stretch=�6�  �� 5�      $ DayButtonFocusVisual��6(   $ false��6F   $ 10��6_   $ 10��6l   
������ $y�	Collapsed=�6�   5�      5�      5�      5�      �� 5�   
   "  63   .�5�      ��  � ��$ 0,3,0,3q�6'   �� 5�       � �� 5�      �� 5�      "  6&    5�      ���� 5�      (��       6+   **(CalendarItem.DayTitleTemplateResourceKey+).R�5�   "   �� 
�������$��Verdana)�6?   $
��Bold"�6T   $	��9.5(�6f   $��Center=�6u   $��0,6,0,6q�6�   $��Center=�6�   ��6�   �� 6�   5�      5�      .R�5�      � $ 	PART_Root��6     5�      ���� 5�   "   DisabledColor $ 	#A5FFFFFF��6H   5�       5�        5�   "   CommonStates��. 5�   &     
Normal��62     5�   &   Disabled��.	 5�   *   �� .��5�   .   W� $
 0D�6>   $ PART_DisabledVisual��6K    16w   $ Opacity�6~   5�   +   5�   '   5�   #   .+�5�      �� #��z��6%   #��z��6O   #��z��6{   $ 1��6�   ._�5�   "   �� ���	#FFFFFFFF$��0q�6A   $ 1��6U   ._�5�   &   �   5�   *   ���� 5�   .   PreviousButtonTemplate " ��6]   .R�5�   2   �� $
 Hand��69   $ 10��6G   
������� 5�   6     5�   :   CommonStates��. 5�   >     
Normal��6J     5�   >   	MouseOver��.	 5�   B   �� .��5�   F   �� $
 0D�6U   $
 path��6b    	#FF73A9D86   $* $(Shape.Fill).(SolidColorBrush.Color)�6�   5�   C   5�   ?     5�   >   Disabled��.	 5�   B   �� .��5�   F   W� $
 0D�6V   $
 path��6c    .56�   $" (Shape.Fill).(Brush.Opacity)�6�   5�   C   5�   ?   5�   ;   ._�5�   6   � .+�5�   :   F� $
 path��6?   "�����5���Ѱ�5h�W��<�h�	
���   �$��Center=�6�   $��10��6�   $
 Fill=�6�   $��Center=�6�   $��6��6�   5�   7   5�   3   5�   /   �� 5�   .   NextButtonTemplate " ��6Y   .R�5�   2   �� $
 Hand��69   $ 10��6G   
������� 5�   6     5�   :   CommonStates��. 5�   >     
Normal��6J     5�   >   	MouseOver��.	 5�   B   �� .��5�   F   �� $
 0D�6U   $
 path��6b    	#FF73A9D86   $* $(Shape.Fill).(SolidColorBrush.Color)�6�   5�   C   5�   ?     5�   >   Disabled��.	 5�   B   �� .��5�   F   W� $
 0D�6V   $
 path��6c    .56�   $" (Shape.Fill).(Brush.Opacity)�6�   5�   C   5�   ?   5�   ;   ._�5�   6   � .+�5�   :   F� $
 path��6?   "����xT��!��xT���S�h4 	
���   �$��Center=�6�   $��10��6�   $
 Fill=�6�   $��Center=�6�   $��6��6�   5�   7   5�   3   5�   /   �� 5�   .   HeaderButtonTemplate " ��6[   .R�5�   2   � $
 Hand��67    5�   6     5�   :   CommonStates��. 5�   >     
Normal��6J     5�   >   	MouseOver��.	 5�   B   �� .��5�   F   �� $
 0D�6U   $ buttonContent��6b    	#FF73A9D86�   $6 0(TextElement.Foreground).(SolidColorBrush.Color)�6�   5�   C   5�   ?     5�   >   Disabled��.	 5�   B   �� .��5�   F   W� $
 0D�6V   $ buttonContent��6c    .56�   $ Opacity�6�   5�   C   5�   ?   5�   ;   .+�5�   6   �� $ buttonContent��6G   #��z��6^   #��z��6�   
���   �#��z��6�   $��Center=�6  5�   3   5�   /   5�   +    5�   *   �� 5�   .   $
��auto �6?   $��79��6L   �� 5�   .   $��* �6?   �� 5�   .   $
��auto �6?     5�   *   �� 5�   .   $
��Auto �6<   �� 5�   .   $��* �6<   .+�5�   *   �� $! PART_HeaderButton��61   $��0��6L   $|�False��6\   $
��Bold"�6n   $
��10.5(�6�   $��Center=�6�   $��Center=�6�   $��0��6�   $��10q�6�   8��  6�   �� 5�   *   $��2��65   $" 
Horizontal=�6E   $��Center=�6^   $��10q�6{   .+�5�   .   �� $! PART_PreviousButton��65   $|�False��6R   $��Center=�6d   $��20��6�   8�� $��28��6�   �� 5�   .   $��10��69   $y�Hidden=�6D   �� 5�   .   $! PART_NextButton��65   $|�False��6N   $��Center=�6`   $��20��6}   8�� $��28��6�   5�   +   � 5�   *   $ PART_MonthView��6/   $��3��6G   $��Center=�6[   $��6,-1,6,6q�6x   $��1��6�   $y�Visible=�6�    5�   .   �� 5�   2   $
��Auto �6C   �� 5�   2   $
��Auto �6C   �� 5�   2   $
��Auto �6C   �� 5�   2   $
��Auto �6C   �� 5�   2   $
��Auto �6C   �� 5   2   $
��Auto �6C   �� 5  2   $
��Auto �6C     5  .   �� 5  2   $
��Auto �6@   �� 5  2   $
��Auto �6@   �� 5  2   $
��Auto �6@   �� 5  2   $
��Auto �6@   �� 5  2   $
��Auto �6@   �� 5	  2   $
��Auto �6@   �� 5
  2   $
��Auto �6@   5  +   � 5  *   $ PART_YearView��6/   $��3��6F   $��Center=�6Z   $��6,-3,7,6q�6w   $��1��6�   $y�Hidden=�6�    5  .   �� 5  2   $
��Auto �6C   �� 5  2   $
��Auto �6C   �� 5  2   $
��Auto �6C   �� 5  2   $
��Auto �6C     5  .   �� 5  2   $
��Auto �6@   �� 5  2   $
��Auto �6@   �� 5  2   $
��Auto �6@   5  +   5  '   5  #   5     �� 5     $ PART_DisabledVisual��6(   8�� $ 0Q�6k   $ 2��6w   $ 2��6�   8�� $
 Fill=�6�   $��1��6�   $y�	Collapsed=�6�   5     # 5     P� 5      $ � {�$% False��6;   . �5!  "   �� $& PART_DisabledVisual��6?    � y�$ Visible=�6`   5"     �� 5#     $
' Year��6�   ( 6*   �� *DisplayMode+) �� *FindAncestor+"*  .a�5$  "   �� $& PART_MonthView��6?    � y�$ Hidden=�6[   �� 5%  "   $& PART_YearView��6?    � y�$ Visible=�6Z   5&     �� 5'     $' Decade��6�   ( 6*   �� *DisplayMode+) �� *FindAncestor+"*  .a�5(  "   �� $& PART_MonthView��6?    � y�$ Hidden=�6[   �� 5)  "   $& PART_YearView��6?    � y�$ Visible=�6Z   5*     5,     5.     5/     50     .��51     �� .+�52  
     -   5        +  Name$+ 
MyCalendar��56     ,   CalendarItemStyle#, �  52     -   CalendarDayButtonStyle#- �   53     $��0q�54     
������ 56     57     58     