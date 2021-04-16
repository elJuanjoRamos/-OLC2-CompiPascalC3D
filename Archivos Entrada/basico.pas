program basico;

    const
        V = true;
        F = false;
        datos = 3;

    var
        val1 : integer = 0;
        val2 : integer = 0;
        val3 : integer = 0;
        resp : integer = 0;
        a : integer = 0;
        b : integer = 0;



procedure ImprimirValores();
begin
    writeln('-----------------------');
    write('El valor de V es: ');
    writeln(V);
    write('El valor de F es: ');
    writeln(F);
    write('El valor de datos es: ');
    writeln(datos);
    write('El valor de val1 es: ');
    writeln(val1);
    write('El valor de val2 es: ');
    writeln(val2);
    write('El valor de val3 es: ');
    writeln(val3);
    write('El valor de resp es: ');
    writeln(resp);
    write('El valor de a es: ');
    writeln(a);
    write('El valor de b es: ');
    writeln(B);
    writeln('-----------------------');
end;


procedure Inicio();
begin
    writeln('----------------------');
    writeln('----ARCHIVO BASICO----');
    writeln('----------------------');

    ImprimirValores();

    
    writeln('----------------------------------------');
    writeln('----ESPEREMOS QUE HAYA FUNCIONADO :D----');
    writeln('----------------------------------------');
end;

begin
    Inicio();
end.
