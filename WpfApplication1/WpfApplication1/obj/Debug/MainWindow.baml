   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfApplication1WpfApplication1  5   	        WpfApplication1.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfApplication1     5   	     � Title$  
MainWindow��5   	   $	��600��6   $
��1000��6)    � 	Resources 5	       �� 
TargetType   ImageButton �� BasedOn �� Property �� Value �� 
TargetType �� Padding �� CornerRadius	 �� Triggers
 P� Property P� Value��%P  &	        7 ��� 5
   
   " ��6   8   .�5      ��  �@�FocusVisualStyle 61   Z� �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   #��z��5      # z��5      #��z��5      $ 0��5      #��z��5      ._�5      �� $��Center=�6/   $��Center=�6L   5      	 5      P� 5      
 �@J�IsMouseOver . . �5   "   ��  � ��
 �����6@   5      P� 5      
 � {� .  . �5   "   ��  �@J�Opacity$
 0.25Q�6<   �� 5   "    � ��
 ���� 6@   5      5       5"      5#      5$      .��5%      �  � RowDefinitions 5&   
   �� 5'      $
��469* �6   �� 5(      $	��80* �6   �� 5)      $	��20* �6    � ColumnDefinitions 5+   
   �� 5,       5.   
   ��%    50      .+�51   
   �� -   5         ��Name$ comboBox��51      $
��Left=�6%   $��
721,72,0,0q�6@   $	��Top=�6T   $	��178��6l   .@�52      �� -   5        $
��cbi1��52      .��6'   Solid6.   �� 53      -   5        $
��cbi2��53      .��6'   Solid Breathing68   �� 54      -   5        $
��cbi3��54      .��6'   Solid Rainbow66   �� 55      -   5        $
��cbi4��55      .��6'   Rotating Rainbow69   56      �� 57   
   -   5         ��Name$ label��57      $��Preset Effects��6   $
��Left=�68   $��
770,45,0,0q�6S   $	��Top=�6g   $��81��6   �� 58   
   -   5        $ label1��58      $��Color Representation��6    $
��Left=�6?   $��721,275,0,0q�6Z   $	��Top=�6o   �� 59   
   -   5         ��Name$ slider��59      $
��Left=�6!   $��125,114,0,0q�6<   $	��Top=�6Q   $	��252��6i    � Maximum$	 100Q�6u    � Minimum$ 1Q�6�    � SmallChange$ 1Q�6�    � LargeChange$ 0Q�6�   6�   �� 5:   
   -	   5        $ 
labelSpeed��5:      $��Speed��6$   $
��Left=�64   $��
228,88,0,0q�6O   $	��Top=�6c    J� RenderTransformOrigin$ 1.072,0.342&�6{   �� 5;   
   -
   5         ��Name$ 	rectOuter��5;      
���   �$
��Left=�64   $	��336��6O   $��
75,175,0,0q�6\   
���   �$��0��6   $	��Top=�6�   $	��361��6�   $��2��6�   �� 5<   
   -   5        $ 	rectInner��5<      $
��Left=�6'   $	��306��6B   $��
90,190,0,0q�6O   $��0��6c   $	��Top=�6w   $	��331��6�   
�������$��2��6�   �� 5=   
   -   5        $ rectRed��5=      ���	#7FFF0000$
��Left=�66   $��45��6Q   $��
97,263,0,0q�6]   
���   �$��0��6�   $	��Top=�6�   $	��315��6�   �� 5>   
   -   5        $ 	rectGreen��5>      ���	#7F00FF00$
��Left=�68   $��45��6S   $��
97,313,0,0q�6_   
���   �$��0��6�   $	��Top=�6�   $	��315��6�   �� 5?   
   -   5        $ rectBlue��5?      ���	#7F0000FF$
��Left=�67   $��45��6R   $��
97,363,0,0q�6^   
���   �$��0��6�   $	��Top=�6�   $	��315��6�   �� 5@   
   -   5         ��Name$ buttonUpdate��5@      $��PREVIEW��6'   $
��Left=�69   $��191,429,0,0q�6T   $	��Top=�6i   $	��125��6�   $��54��6�   $��2��6�   �� 5A   
   -   5         ��Name$ 
textValRed��5A      $
��Left=�6&   $��23��6A   $��108,275,0,0q�6M    �� TextWrapping$
 Wrap=�6b   $��0��6v   $	��Top=�6   $��54��6�   6�   �� 5B   
   -   5        $ textValGreen��5B      $
��Left=�6(   $��23��6C   $��108,323,0,0q�6O   $
 Wrap=�6d   $��0��6x   $	��Top=�6�   $��54��6�   6�   �� 5C   
   -   5        $ textValBlue��5C      $
��Left=�6'   $��23��6B   $��108,374,0,0q�6N   $
 Wrap=�6c   $��0��6w   $	��Top=�6�   $��54��6�   6�   �� 5D   
   -   5        $ 	sliderRed��5D      $
��Left=�6$   $��177,275,0,0q�6?   $	��Top=�6T   $��23��6l   $	��221��6x   $	 255Q�6�   $ 1Q�6�   $ 0Q�6�    �� IsSnapToTickEnabled .  � Value 6�   ��  �� ElementName$ 
textValRed�� �� Path$
 Text�6�   �� 5E   
   -   5        $ sliderGreen��5E      $
��Left=�6&   $��177,323,0,0q�6A   $	��Top=�6V   $��23��6n   $	��221��6z   $	 255Q�6�   $ 1Q�6�   $ 0Q�6�    .  6�   �� $ textValGreen��$
 Text�6�   �� 5F   
   -   5        $ 
sliderBlue��5F      $
��Left=�6%   $��177,374,0,0q�6@   $	��Top=�6U   $��23��6m   $	��221��6y   $	 255Q�6�   $ 1Q�6�   $ 0Q�6�    .  6�   �� $ textValBlue��$
 Text�6�   �� 5G   
   
���   � �� RadiusX$ 20��6!    �� RadiusY$ 20��6.   $
��Left=�6;   $	��214��6V   $��634,297,0,0q�6c   
���   �$��0��6�   $	��Top=�6�   $	��291��6�   $��2��6�   �� 5H   
   .+�5I      �� -   5        $ rectTL��5I      
���  ��$��37��63     �� Left$	  672��6?   
! �� Top$	! 320��6Q   $��37��6b   �� 5J      -   5        $ rectT��5J      
���  ��$	  709��62   $��38��6D   $	! 320��6P   $	��148��6a   $
��Left=�6m   $	��Top=�6�   �� 5K      -   5        $ rectTR��5K      
���  ��$��37��63   $	  857��6?   $	! 320��6Q   $��37��6b   �� 5L      -   5        $ rectL��5L      
���  ��$��96��62   $	  672��6>   
���   �$��0��6_   $	! 357��6s   $��37��6�   �� 5M      -   5        $ rectBL��5M      
���  ��$��37��63   $	  672��6?   
���   �$��0��6`   $	! 453��6t   $��37��6�   �� 5N      -   5        $ rectBR��5N      
���  ��$��37��63   $	  857��6?   
���   �$��0��6`   $	! 453��6t   $��37��6�   �� 5O      -   5        $ rectB��5O      
���  ��$��37��62   $	  709��6>   
���   �$��0��6_   $	! 453��6s   $	��148��6�   �� 5P      -   5        $ rectR��5P      
���  ��$��96��62   $	  857��6>   
���   �$��0��6_   $	! 357��6s   $��37��6�   �� 5Q      -   5        $ rectGlow��5Q      
���   �$	��170��67   $	  672��6D   
���   �$��0��6e   $	! 320��6y   $	��222��6�   " J� OpacityMask" 5R      �� 5S      # �� ImageSource$# OpacityMaskInvert.png��6!   5U      � 5V      -   5        $ �Name$$ colorButtons��5V      $	! 216��6)   $	��294��6:   $	��274��6F   $	  110��6S   .+�5W      �� -    5        $ 	btnColor1��5W      $��1��6,   $	  472��68   $! 98��6J   $��32��6Z   $��32��6e   $��20,8,242,234q�6q   6�   �� 5X      -!   5        $ 	btnColor2��5X      $��2��6,   $	  509��68   $! 98��6J   $��32��6Z   $��32��6e   $��57,8,205,234q�6q   6�   �� 5Y      -"   5        $ 	btnColor3��5Y      $��3��6,   $	  546��68   $! 98��6J   $��32��6Z   $��32��6e   $��94,8,168,234q�6q   6�   �� 5Z      -#   5        $ 	btnColor4��5Z      $��4��6,   $	  583��68   $! 98��6J   $��32��6Z   $��32��6e   $��131,8,131,234q�6q   6�   �� 5[      -$   5        $ 	btnColor5��5[      $��5��6,   $
��Left=�68   $��	168,8,0,0q�6S   $	��Top=�6f   $��32��6~   $��32��6�   6�   �� 5\      -%   5        $ 	btnColor6��5\      $��6��6,   $
��Left=�68   $��	205,8,0,0q�6S   $	��Top=�6f   $��32��6~   $��32��6�   6�   �� 5]      -&   5        $ 	btnColor7��5]      $��7��6,   $
��Left=�68   $��	242,8,0,0q�6S   $	��Top=�6f   $��32��6~   $��32��6�   6�   �� 5^      -'   5        $ 	btnColor8��5^      $��8��6,   $
��Left=�68   $��
242,45,0,0q�6S   $	��Top=�6g   $��32��6   $��32��6�   6�   �� 5_      -(   5        $ 	btnColor9��5_      $��9��6,   $
��Left=�68   $��
242,82,0,0q�6S   $	��Top=�6g   $��32��6   $��32��6�   6�   �� 5`      -)   5        $ 
btnColor10��5`      $��10��6-   $
��Left=�6:   $��242,119,0,0q�6U   $	��Top=�6j   $��32��6�   $��32��6�   6�   �� 5a      -*   5        $ 
btnColor11��5a      $��11��6-   $
��Left=�6:   $��242,156,0,0q�6U   $	��Top=�6j   $��32��6�   $��32��6�   6�   �� 5b      -+   5        $ 
btnColor12��5b      $��12��6-   $
��Left=�6:   $��242,193,0,0q�6U   $	��Top=�6j   $��32��6�   $��32��6�   6�   �� 5c      -,   5        $ 
btnColor13��5c      $��13��6-   $
��Left=�6:   $��242,230,0,0q�6U   $	��Top=�6j   $��32��6�   $��32��6�   6�   �� 5d      --   5        $ 
btnColor14��5d      $��14��6-   $
��Left=�6:   $��205,230,0,0q�6U   $	��Top=�6j   $��32��6�   $��32��6�   6�   �� 5e      -.   5        $ 
btnColor15��5e      $��15��6-   $
��Left=�6:   $��168,230,0,0q�6U   $	��Top=�6j   $��32��6�   $��32��6�   6�   �� 5f      -/   5        $ 
btnColor16��5f      $��16��6-   $
��Left=�6:   $��131,230,0,0q�6U   $	��Top=�6j   $��32��6�   $��32��6�   6�   �� 5g      -0   5        $ 
btnColor17��5g      $��17��6-   $
��Left=�6:   $��
94,230,0,0q�6U   $	��Top=�6i   $��32��6�   $��32��6�   6�   �� 5h      -1   5        $ 
btnColor18��5h      $��18��6-   $
��Left=�6:   $��
57,230,0,0q�6U   $	��Top=�6i   $��32��6�   $��32��6�   6�   �� 5i      -2   5        $ 
btnColor19��5i      $��19��6-   $
��Left=�6:   $��
20,230,0,0q�6U   $	��Top=�6i   $��32��6�   $��32��6�   6�   �� 5j      -3   5        $ 
btnColor20��5j      $��20��6-   $
��Left=�6:   $��
20,193,0,0q�6U   $	��Top=�6i   $��32��6�   $��32��6�   6�   �� 5k      -4   5        $ 
btnColor21��5k      $��21��6-   $
��Left=�6:   $��
20,156,0,0q�6U   $	��Top=�6i   $��32��6�   $��32��6�   6�   �� 5l      -5   5        $ 
btnColor22��5l      $��22��6-   $
��Left=�6:   $��
20,119,0,0q�6U   $	��Top=�6i   $��32��6�   $��32��6�   6�   �� 5m      -6   5        $ 
btnColor23��5m      $��23��6-   $
��Left=�6:   $��	20,82,0,0q�6U   $	��Top=�6h   $��32��6�   $��32��6�   6�   �� 5n      -7   5        $ 
btnColor24��5n      $��24��6-   $
��Left=�6:   $��	20,45,0,0q�6U   $	��Top=�6h   $��32��6�   $��32��6�   6�   5o      �� 5p      -8   5        $ buttonReset��5p      $��Reset��6*   $	  187��6:   $	! 275��6L   $	��146��6]   $��30��6�   �� 5q      -9   5        $ btnDirection��5q      $��Reverse��6+   $	  187��6=   $	! 320��6O   $	��146��6`   $��30��6l   6x   5r      �� 5s   
   -:   5        $ 
rectCenter��5s      
���   �$
��Left=�65   $	��100��6P   $��706,355,0,0q�6]   
���   �$��0��6�   $	��Top=�6�   $	��152��6�   �� 5t   
   -;   5        % ��Name$% 
textStatus��5t      $
��Left=�6(   $��
859,74,0,0q�6C   $
��Wrap=�6W   $��
Waiting...��6k   & �� TextAlignment$& Right=�6}   $	��Top=�6�   $	��123��6�   $��1��6�   $��2��6�   �� 5u   
   -<   5        $% textVersion��5u      $
��Left=�6)   $��
859,58,0,0q�6D   $
��Wrap=�6X   $��	ver 1.0.0��6l   $	��Top=�6}   $& Right=�6�   $	��123��6�   $��1��6�   �� 5v   
   -=   5        $ buttonToPad��5v      #��[  6&   $��
UPDATE PAD��6K   $
��Left=�6`   $��706,355,0,0q�6{   $	��Top=�6�   $	��152��6�   $	��100��6�   {�.  ���	#FF232323���	#FFDDDDDD6  5w      5x      