   M S B A M L   `   `   `  ����D  @ServerWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ServerWPF	ServerWPF  5   	       ServerWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ServerWPF     5   	     � Title$  Сервер��5   	   $��326.749��6   $��363.573��6)   .��5
      � .+�5   
   �� -   5         ��Name$ label1��5      $2��,Укажите IP-адрес сервера ��6    $
��Left=�6D   $��2,1,0,0q�6_   $	��Top=�6p   �� 5   
   -   5         ��Name$ 	IpAddress��5      $
��Left=�6%   $��23��6@   $��9,28,0,0q�6L    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��143��6�   $��	127.0.0.1��6�   �� 5   
   -   5        $ label2��5      $��Номер порта ��6    $
��Left=�67   $��	152,2,0,0q�6R   $	��Top=�6e   �� 5   
   -   5        $
 Port��5      $
��Left=�6    $��23��6;   $��
159,28,0,0q�6G   $
 Wrap=�6[   $	��Top=�6o   $��79��6�   $
��8888��6�   �� 5   
   -   5         ��Name$ listBox��5      $
��Left=�6#   $	��202��6>   $��
130,56,0,0q�6K   $	��Top=�6_   $	��211��6w   �� 5   
   -   5         ��Name$ button��5      $��Подключиться��6!   $
��Left=�68   $��
243,28,0,0q�6S   $	��Top=�6g   $��98��6   $��23��6�   6�   �� 5   
   -   5        $ 	buttonDis��5      $��Отключиться��6$   $
��Left=�6:   $��243,263,0,0q�6U   $	��Top=�6j   $��98��6�   6�   �� 5   
   -   5        $ listBoxUsers��5      $
��Left=�6(   $	��202��6C   $��	10,56,0,0q�6P   $	��Top=�6c   $	��115��6{   5      5      