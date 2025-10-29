CREATE TABLE registros (

    

    numero1 float NOT NULL,
    numero2 float NOT NULL,
    

    resultado_suma float,
    resultado_resta float,
    resultado_multiplicacion float,
    resultado_division float, 
    

    fecha_operacion DATETIME DEFAULT GETDATE()
);