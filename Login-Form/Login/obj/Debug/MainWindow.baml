   M S B A M L   `   `   `  ����D  @LoginForm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:LoginForm	LoginForm  5   	       LoginForm.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:LoginForm     5   	     � WindowStartupLocation$  CenterScreen=�5       � WindowStyle$
 None=�6?    � AllowsTransparency . 
������  � Title$ 
MainWindow��5	   	   $	��500��6   $	��800��6)   .��5      � $
��Left=�6   $	��800��6&    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��1.5* �6   .+�5   
   �� -   5        $#��E:\LoginForm\Images\Close.png��5      $��1��67    K� ZIndex$ 1��6G      imgClose#��[  6p   �� 5   
    �� CornerRadius$ 	10,0,0,10��6   ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5      � 	 � Color$	 
LightGreen��6#   
 � Offset$
 0Q�66   � 5      $	 LightGoldenrodYellow��6#   $
 1Q�6@   5      ._�5      � .+�5      �� .+�5      �  � Points �    -1��� ;X��
���`���6<   � 5      ' �    �� �� ���� ��
��
���`���6?   A� 5      $��250 450 0 0q�6   $��40��63   $��40��6>   
���`���6J   A� 5      $��
50 400 0 0q�6   $��20��62   $��20��6=   
���`���6I   5       �� 5"      $��Center=�6   $��Center=�68   $��20q�6U   .+�5#      �� $��Login��6      	titleText#��[ 6-   �� 5$      $&�� Enter your Username and Password��6      
normalText#��[ 6H    J� Opacity$	 0.8Q�6l   $��20 30q�6z   �� 5%      $��Login��6    
 button#��[ 6-   5&      5'      5)      �� 5*   
   $%��Images/Benjamin transparent.png��6   $	��100��69   $	��100��6E   $
��Left=�6R   $	��Top=�6m   �� 5+   
   -   5        ���#ffffff$��1��5+   &   $ 	0 10 10 0��66   ._�5,      �� .+�5-      �� $��Login to App��6   #��[ 60   
���"�"�6S   �� 5.       �� Orientation$ 
Horizontal=�6   $
��0 20q�66   $��Center=�6D   .+�5/      ��   buttonCircle#��[ 6   $��5q�6C   .��50      �� $&�� E:\LoginForm\Images\Facebook.png��6    $��20��6J   $��20��6U   51      �� 53      #��[ 6   $��5q�6C   .��54      �� $+��%E:\LoginForm\Images\google-better.png��6"   $��20��6Q   $��20��6\   55      56      �� 58      $��or use your email info: ��6   #��[ 6<   ���#878787$��	0 10 0 15q�6u   �� 5:      $��1q�6   ���#acb0af$
��70 7q�6C   $ 5��6Q   ._�5;      � $	��7 9q�6    5<      �� 5=      $
��auto �6/   �� 5>      $��* �6/   .+�5A      �� $#��E:\LoginForm\Images\Email.png��6    $��20��6G   �� 5B      -   5         ��Name$ 	textEmail��5B   $   $��Email��6W     textHint#��[ 6d   �� 5C      -   5         ��Name$ txtEmail��5C   "     textBox#��[ 6W   5D      5F      �� 5H      $��1q�6   ���#acb0af$
��70 7q�6C   $ 5��6Q   ._�5I      � $	��7 9q�6    5J      �� 5K      $
��auto �6/   �� 5L      $��* �6/   .+�5O      �� $"��E:\LoginForm\Images\Lock.png��6    $��20��6F   �� 5P      -   5        $ textPassword��5P   $   $��Password��6]   #��[ 6m   G� 5Q      -   5         G�Name$ txtPassword��5Q   &   #��[ 6i   5R      5S      �� 5U      -   5        $��Sign in��5U        
mainbutton#��[ 6@   5W      5X      5Z      5[      