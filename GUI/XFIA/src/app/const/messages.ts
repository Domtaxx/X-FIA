export class alertMessages{
    //http request status
    public static successHeader='Exito';
    public static rejected='No se ha podido procesar la solicitud';
    //tournaments 
    public static sucessTournament='Se ha agregado con éxito, la llave generada es ';
    public static simultaneousTournaments='No se ha podido agregar el campeonato, recuerde que no es posible que existan dos campeonatos ocurriendo en fechas simultaneas'

    //races 
    public static raceSuccess='La carrera ha sido agregada al campeonato seleccionado' ;
    public static duplicatedRaceName='el nombre indicado ya ha sido usado para otra carrera en el torneo seleccionado';
    public static outsideBoundsRace='recuerde que las fechas de la carrera deben estar dentro de los limites del campeonato';
    public static simultaneousRace='Ya existen carreras activas en el periodo indicado';

    //Common Error
    public static serverInterrupt='No se ha podido conectar con el servidor';

    //Date/Time Fields Error
    public static rejectedDateHeader="Fechas Invalidas"
    public static invalidDateElapse="La fecha final debe ser mayor o igual a la fecha inicial";
    public static invalidTimeElapsed="Si la competencia inicia y termina el mismo dia, la hora inicial debe ser menor a la fecha final"
    public static inThePast="No es posible seleccionar fechas en el pasado"
    public static invalidFieldsHeader="Errores en los campos";
    public static invalidFieldsBody='Alguno de los campos indicados no cumple con las reglas, por favor revisar el texto bajo los cuadros'
    //File user register
    public static rejectedTeamTabHeader="No se puede avanzar"
    public static rejectedTeamTabBody="Existen campos con errores, por favor revise el texto en rojo"
    public static allowedTeamCreation="Se ha agregado el usuario con exito"
    public static accountAlreadyExists="El correo indicado ya tiene una cuenta asociada"
    public static teamAlreadyExists="Ya existe una escuderia con el nombre indicado"
    public static repeatedSubName="El nombre de ambos equipos no puede ser el mismo"
    public static emptyPilots="Existen Pilotos Vacios";
    public static reapetedPilots="Existen Pilotos Repetidos";
    public static emptyCar="Debe seleccionar un auto"
    public static outBoundBudget="Presupuesto Fuera de Rango"

    //create private league
    public static privateLeagueCreateHeader='Crear Liga Privada';
    public static privateAcceptCreate="Aceptar";

    public static privateLeagueCreatedBody="Su liga privada ha sido creada con exito, El código de la liga es"
    public static privateLeagueFormatErrorHeader="La llave no cumple con las condiciones"
    public static privateLeagueFormatErrorBody="Recuerde que debe indicar una llave alfanúmerica con 12 digitos de largo"
    public static privateLeagueLeaveSucessBody='Ha abandonado la liga con éxito'
    //File Error
    public static rejectedImageFileHeader="Formato Incompatible"
    public static rejectedImageFileBody="Solo estan permitidos los archivos .jpg y .png"

}