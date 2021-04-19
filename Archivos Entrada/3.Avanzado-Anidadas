program anidadas;
var
	w : integer = 1;
	x : integer = 2;
	y : integer = 3;
	z : integer = 4;
  procedure p1(a : integer; var b : integer);
    var
		w : integer = 11;
        x : integer = 12;
		y : integer = 13;
	procedure p11();
		var
			w : integer = 21;
			x : integer = 22;
		procedure p111();
    		var
				w : integer = 31;
			begin
				writeln('Local 31 = ',w);
				writeln('Ambito Padre 22 = ',x);
				writeln('Ambito Padre de Padre 13 = ',y);
				writeln('Global 4 = ',z);
				writeln('Parametro por valor de Padre de Padre 1 = ',a);
				writeln('Parametro por referencia de Padre de Padre 2 = ',b);
				b := 1000;
			end;
    	begin
			p111();
        end;
  begin
      p11();
  end;
  procedure p11();
  begin
	writeln('Aqui no debe entrar');
  end;
begin
	writeln('Valor Antes 2 = ',x);
	p1(1,x);
	writeln('Valor Despues 1000 = ',x);
end.