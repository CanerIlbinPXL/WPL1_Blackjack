   M S B A M L   `   `   `  ����N  JBlackjackApplicatie, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:BlackjackApplicatieBlackjackApplicatie  5   	   $    BlackjackApplicatie.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:BlackjackApplicatie     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��600��6)   .��5	      � ��5
   
   �� 5       �� ImageSource$y sC:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\gridbg2.jpg��6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $��0��6   
�������$��Speler��63   $��17(�6D   $��Center=�6R   $��Center=�6o   �� 5   
   $��2��6   $
��Bank��6    $��17(�6/   
�������$��Center=�6Q   $��Center=�6n   �� 5    
   -   5         ��Name$ LblscoreSpeler��5    [   $��Center=�6   $��Center=�6+   
�������$��0��6s   $��7��6�   $��0��6�   $��17(�6�   �� 5!   
   -   5        $ LblscoreBank��5!   \   $��Center=�6   $��Center=�6+   
�������$��2��6r   $��7��6�   $��0��6�   $��17(�6�   �� 5"   
   -   5         ��Name$ BtnDeel��5"      $��1��6"   $
��Deel��62   $	��135��6A   {�.  $��30��6_   6k   �� 5#   
   -   5        $ BtnHit��5#      $��1��6!   $��1��61   $	��Hit��6>   $	��135��6L   $��30��6X   {�.  6v   �� 5$   
   -   5        $ BtnStand��5$      $��1��6#   $��2��63   $��Stand��6@   $	��135��6P   $��30��6\   {�.  6z   �� 5%   
   -   5        $ BtnDoubleDown��5%      $��1��6(   $��3��68   $��Double down��6E   $	��135��6[   $��30��6g   {�.  6�   �� 5&   
   -   5         ��Name$ 	txtSpeler��5&       �� TextWrapping$
 Wrap=�6%   $��0��69   $��1��6I   $��3��6V   $�� ��6g   �� 5'   
   -   5        $ txtBank��5'      $
 Wrap=�6#   $��2��67   $��1��6G   $��3��6T   $�� ��6e   �� 5(   
   -	   5        $ LblResultaat��5(      $��Center=�6&   $��Center=�6C   $��1��6^   $��7��6n   $�� ��6{   $��17(�6�   �� 5)   
   $��Inzet:��6   
�������$��Center=�65   $��4��6S   $��Center=�6`   �� 5*   
   -
   5        $ txtInzet��5*      {�.  $��Right=�66   $��4��6S   $
 Wrap=�6`   $��Center=�6�   $��45��6�   ��6t   ��  �� ElementName$ InzetSlider��	 �� Path$	 Value�
 �� UpdateSourceTrigger$
 PropertyChanged=�6�   �� 5+   
   -   5         ��Name$ InzetSlider��5+      {�.   �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q�6T   $��1��6g   $��Center=�6w   $��4��6�   $��Center=�6�   $	��120��6�   �� 5,   
   $��	Kapitaal:��6   
�������$��Center=�68   $��5��6W   $��Center=�6d   �� 5-   
   -   5        $ txtKapitaal��5-      {�.  $��Right=�69   $��5��6W   $
 Wrap=�6d   $�� ��6x   $��Center=�6�   $��45��6�   �� 5.   
   -   5        $ BtnNieuw��5.      $��1��6#   $	��135��63   $��30��6?   $��
Nieuw spel��6K   $��5��6b   {�. 6�   �� 5/   
   $��Laatste kaart��6   
�������$��Center=�6<   $��6��6Y   $��Center=�6f   $��2��6�   �� 50   
   -   5        $ BtnLaatstekaartSpeler��50      $�� ��60   $��6��6<   $��2��6I   $��80��6Z   $��55��6f   $��Center=�6q   $��Right=�6�   ��51      �� 52      $| v C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\card back.png��6   54      �� 55   
   -   5        $ BtnLaatstekaartBank��55      $�� ��6.   $��6��6:   $��2��6G   $��2��6W   $��80��6h   $��55��6t   $��Center=�6   $
��Left=�6�   ��56      �� 57      $| v C:\Users\Caner\source\repos\CanerIlbinPXL\WPL1_Blackjack\BlackjackApplicatie\BlackjackApplicatie\Assets\card back.png��6   59      �� 5:   
   $��2��6   
�������$��Laatste kaart��63   $��6��6K   $��Center=�6X   $��Center=�6s   $��2��6�   �� 5;   
   
�������$��9��6+   $��3��68   5=      �� 5>   
   -   5        $��lblTijdstip��5>   0   $��9��6   
���   �$�� ��6C   �� 5?   
   -   5        $��btnHistoriek��5?      $��9��6%   $��1��62   $��60��6B   $��25��6M   $��	Historiek��6Z   6n   5B      5C      