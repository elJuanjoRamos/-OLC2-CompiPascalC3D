﻿program intermedios;
    var compare : integer = 56;
    var last : integer = 2;
    var index : real = 0.0;
    var i: integer;
    var tabla, numero: integer;
begin
    writeln('**  IF ***');
    
    if compare > 50 then
        begin
            writeln('IF CORRECTO');
        end
    else if compare = 56 then
        begin
            writeln('IF INCORRECTO');
        end
    else
        begin
            writeln('IF INCORRECTO');
        end;
    writeln('*** SWITCH  **');
    
    case last of
        1:
                writeln('SWITCH MALO');        
         2:
                writeln('SWITCH BIEN');
         3:
                writeln('SWITCH MALO');
         else
                writeln('SWITCH MALO');
         end;


    case last of
        1:
                writeln('SWITCH MALO');        
        -2:
                writeln('SWITCH MALO');
        3:
                writeln('SWITCH MALO');
        else
                writeln('SWITCH BIEN');
    end;
    
    writeln('** WHILE **');
    
    while index >= 0 do
    begin
        if index = 0 then
            begin
                index := index + 100;
            end
        else if index > 50 then
            begin
                index := index / 2 - 25;
            end
        else
            begin
                index := (index / 2) - 1;
            end;
        writeln(index);
    end;

    writeln('** REPEAT **');
    i := -1;
    repeat
        i := i + 1;
        if ( (i = 0) OR (i = 1) OR (i = 11) OR (i = 12) ) then
            begin
                writeln('************************************************');
            end
        else if (i = 2) then
            begin
                writeln('*  *****  **         **           **         ***');
            end
        else if ((i >= 3) AND (i <= 5)) then
            begin
                writeln('*  *****  **  *********  *******  **  **********');
            end
        else if (i = 6) then
            begin
                writeln('*  *****  **         **           **  **********');
            end
        else if ((i >= 7) AND (i <= 9)) then
            begin
                writeln('*  *****  ********   **  *******  **  **********');
            end    
        else if (i = 10) then
            begin
                writeln('*         **         **  *******  **         ***');
            end
    until (i > 12);

    for tabla := 1 to 5 do
    begin
        for numero := 1 to 10 do
        begin
            writeln( tabla, ' por ', numero , ' es ', tabla * numero );
        end;
        writeln('');        
    end;

end.

***********************************************************************
***********                 IF                         ****************
***********************************************************************
IF CORRECTO
***********************************************************************
***********                 SWITCH                     ****************
***********************************************************************
SWITCH BIEN
SWITCH BIEN
***********************************************************************
***********                 WHILE                      ****************
***********************************************************************
 1.00000000000000E+002
 2.50000000000000E+001
 1.15000000000000E+001
 4.75000000000000E+000
 1.37500000000000E+000
-3.12500000000000E-001
***********************************************************************
************                 REPEAT                    ****************
***********************************************************************

*********************************************************************************************************
*********************************************************************************************************
**********  ***************  ******                 ******                 ******              **********
**********  ***************  ******  *********************  *************  ******  **********************
**********  ***************  ******  *********************  *************  ******  **********************
**********  ***************  ******  *********************  *************  ******  **********************
**********  ***************  ******                 ******                 ******  **********************
**********  ***************  ********************   ******  *************  ******  **********************
**********  ***************  ********************   ******  *************  ******  **********************
**********  ***************  ********************   ******  *************  ******  **********************
**********                   ******                 ******  *************  ******              **********
*********************************************************************************************************
*********************************************************************************************************
1 por 1 es 1
1 por 2 es 2
1 por 3 es 3
1 por 4 es 4
1 por 5 es 5
1 por 6 es 6
1 por 7 es 7
1 por 8 es 8
1 por 9 es 9
1 por 10 es 10
2 por 1 es 2
2 por 2 es 4
2 por 3 es 6
2 por 4 es 8
2 por 5 es 10
2 por 6 es 12
2 por 7 es 14
2 por 8 es 16
2 por 9 es 18
2 por 10 es 20
3 por 1 es 3
3 por 2 es 6
3 por 3 es 9
3 por 4 es 12
3 por 5 es 15
3 por 6 es 18
3 por 7 es 21
3 por 8 es 24
3 por 9 es 27
3 por 10 es 30
4 por 1 es 4
4 por 2 es 8
4 por 3 es 12
4 por 4 es 16
4 por 5 es 20
4 por 6 es 24
4 por 7 es 28
4 por 8 es 32
4 por 9 es 36
4 por 10 es 40
5 por 1 es 5
5 por 2 es 10
5 por 3 es 15
5 por 4 es 20
5 por 5 es 25
5 por 6 es 30
5 por 7 es 35
5 por 8 es 40
5 por 9 es 45
5 por 10 es 50
}