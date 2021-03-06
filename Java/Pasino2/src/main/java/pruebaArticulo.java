
import java.math.BigDecimal;
import java.sql.*;
import java.text.DecimalFormat;
import java.util.*;

import com.mysql.jdbc.DatabaseMetaData;
import com.sun.org.apache.xerces.internal.impl.xpath.regex.ParseException;

public class pruebaArticulo {
	private enum Action {Salir, Nuevo, Listar, Eliminar, Consultar, Editar}
	public static Scanner tcl = new Scanner (System.in);
	//metodo global para devolver conexion
	
	private static Connection connection;
	
	//metodo global para eliminar una fila
	
	
	
	
	
	//metodos BD
	public static void borrado(Connection connection) throws SQLException{
		lectura(connection);
		System.out.println("");
		System.out.println("Escriba un id para borrar");
		int idBorrar = tcl.nextInt();
		tcl.nextLine();
		System.out.println("Esta seguro de que quiere eliminar la fila correspondiente a :"+idBorrar);
		String respuesta=tcl.nextLine();
		if(respuesta=="si"){
			Statement s = connection.createStatement(); 
			ResultSet rs = s.executeQuery ("delete * from articulo where id="+idBorrar);
		}
		else{
			borrado(connection);
		}
		menu();
			
		
		
		
		

	}
	/*public static void insercion(Connection connection) throws SQLException{
		
		System.out.println("Introduzca nombre :");
		String nombre=tcl.nextLine();
		System.out.println("categoria");
		String categoria = tcl.nextLine();
		System.out.println("inserte el precio");
		double precio = tcl.nextDouble();
		tcl.nextLine();
		
		
		try {
		      // Creamos el PreparedStatement si no estaba ya creado. 
		      	Statement s = connection.createStatement();
		        ResultSet rs = s.executeQuery(
		        		"insert into articulo values ("+nombre+","+categoria+","+String.valueOf(precio)+")");
		      
		   } catch (SQLException e) {
		      e.printStackTrace();
		   }
		
		System.out.println("Quiere visualizar? \n"
				+ "1.SI\n"
				+ "2.NO");
		int respuesta=tcl.nextInt();
		tcl.nextLine();
		if(respuesta == 1){
			lectura(connection);
		}
		else if(respuesta==2){
			menu();
		}
		else{
			System.out.println("prueba otra vez");
		}
		
		
	}*/
	public static void actualizacion(Connection connection) throws SQLException{
		long id = scanLong("	id: ");
		Articulo articulo = scanArticulo();
		try{
			if(updatePreparedStatement==null){
				updatePreparedStatement = connection.prepareStatement(updateSQL);
				updatePreparedStatement.setString(1, articulo.nombre);
				updatePreparedStatement.setLong(2, articulo.categoria);
				updatePreparedStatement.setBigDecimal(3, articulo.precio);
				updatePreparedStatement.setLong(4, id);
				int count = updatePreparedStatement.executeUpdate();
				if(count==1){
					System.out.println("Articulo guardado");
				}
				else{
					System.out.println("No existe un Articulo con ese ID");
				}

			}
		}catch(Exception ex){
			System.out.println(ex);
		}finally{};
		
	}
	public static void lecturaUnica(Connection connection)throws SQLException{
		System.out.println("Introduzca el id");
		int id=tcl.nextInt();
		tcl.nextLine();
		
		Statement s = connection.createStatement();
		ResultSet rs = s.executeQuery("select * from articulo where id="+id);
		
		//java.sql.DatabaseMetaData metaDatos = connection.getMetaData();
				//cogemos la metainformacion de la lectura para trabajar con ella
				ResultSetMetaData rsmd = rs.getMetaData();
				
				//bucle para imprimir el nombre de las columnas
				for (int i = 1; i <= rsmd.getColumnCount(); i++) {
					   System.out.print(rsmd.getColumnName(i)+"\t"+"\t" );
					   
					}
				System.out.println("");
				//System.out.println(prueba.getString(4));
				//el bucle for se ejecutara tantas veces como columnas haya
				//mientras que el while se ejecutara mientras haya algo que leer
				while(rs.next()){
					for(int i=1; i<=rsmd.getColumnCount();i++){
						//para poder hacerlo generico cojo el nombre del campo a buscar de la metainformacion
						System.out.printf(rs.getString(rsmd.getColumnName(i))+"\t"+"\t"+"   ");
						
					}
					System.out.println("");}
				menu();
	}
	public static void lectura(Connection connection) throws SQLException{
		
		//creamos la consulta
		Statement s = connection.createStatement(); 
		ResultSet rs = s.executeQuery ("select * from articulo");
		
		
		//java.sql.DatabaseMetaData metaDatos = connection.getMetaData();
		//cogemos la metainformacion de la lectura para trabajar con ella
		ResultSetMetaData rsmd = rs.getMetaData();
		
		//bucle para imprimir el nombre de las columnas
		for (int i = 1; i <= rsmd.getColumnCount(); i++) {
			   System.out.print(rsmd.getColumnName(i)+"\t"+"\t" );
			   
			}
		System.out.println("");
		//System.out.println(prueba.getString(4));
		//el bucle for se ejecutara tantas veces como columnas haya
		//mientras que el while se ejecutara mientras haya algo que leer
		while(rs.next()){
			for(int i=1; i<=rsmd.getColumnCount();i++){
				//para poder hacerlo generico cojo el nombre del campo a buscar de la metainformacion
				System.out.printf(rs.getString(rsmd.getColumnName(i))+"\t"+"\t");
				
			}
			System.out.println("");
		}
		//cierro la conexion y 
		/*s.close();
		connection.close();*/
		System.out.println("Conexion a la base de datos cerrada");
		menu();
	}
	
	private static PreparedStatement insertPreparedStatement;
	private final static String insertSQL = "insert into articulo (nombre, categoria, precio)"
			+ "values(?, ?, ?)";
	private static PreparedStatement updatePreparedStatement;
	private final static String updateSQL ="update articulo"
			+"set nombre = ?, "
			+"set categoria = ?,"
			+"set precio= ?";
	public static void nuevo(Connection connection)throws SQLException{
		Articulo articulo = scanArticulo();
		try{
		if(insertPreparedStatement == null){
			insertPreparedStatement = connection.prepareStatement(insertSQL);
		}
		insertPreparedStatement.setString(1, articulo.nombre);
		insertPreparedStatement.setLong(2, articulo.categoria);
		insertPreparedStatement.setBigDecimal(3, articulo.precio);
		insertPreparedStatement.executeUpdate();
		System.out.println("articulo guardado");
		}catch(Exception ex){
			System.out.println(ex);
		}
		showArticulo(articulo);
	}
	
	//METODOS CREACION DE DATOS Y COMPROBACION
	
	private static void showArticulo(Articulo articulo){
		System.out.println("     id: " + articulo.id);
		System.out.println("     nombre: " + articulo.nombre);
		System.out.println("     categoria: " + articulo.categoria);
		System.out.println("     precio: " + articulo.precio);
	}
	public static Action menu(){
		while(true){
		System.out.println(" 1. SALIR \n"
				+ "2.NUEVO \n"
				+ "3.LISTAR \n"
				+ "4.ELIMINAR \n"
				+ "5.LEER TODAS \n"
				);
		String action=tcl.nextLine().trim();
		if(action.matches("[123450]"))
			return Action.values()[Integer.parseInt(action)];
		else
			System.out.println("opcion invalida");
		}
		
	}
	private static class Articulo{
		private long id;
		private String nombre;
		private long categoria;
		private BigDecimal precio;
	}
	private static String ScanString(String label){
		System.out.println(label);
		return tcl.nextLine().trim();
		
	}
	private static long scanLong(String label){
		while(true){
			System.out.println(label);
			String data=tcl.nextLine().trim();
			
			try{
				return Long.parseLong(data);
				
			}catch(NumberFormatException ex){
				System.out.println("Introduzca un numero porfavor.");
			}
		}
	}
	private static BigDecimal scanBigDecimal(String label){
		while(true){
		System.out.println(label);
		String data=tcl.nextLine().trim();
		DecimalFormat decimalFormat = (DecimalFormat)DecimalFormat.getInstance();
		decimalFormat.setParseBigDecimal(true);
		
			try {
				return (BigDecimal)decimalFormat.parse(data);
			} catch (java.text.ParseException e) {
				
				System.out.println("Debe ser un numero decimal");
			}
		
		}
	}
	private static Articulo scanArticulo(){
		Articulo articulo = new Articulo();
		articulo.nombre=ScanString(		"	nombre: ");
		articulo.categoria=scanLong(	"	categoria: ");
		articulo.precio=scanBigDecimal(		"	precio: ");
		return articulo;
	}
	
	
	
	private static void closePreparedStatement() throws SQLException{
		if(insertPreparedStatement != null)
			insertPreparedStatement.close();
	}
	
	
	public static void main(String args [])throws SQLException{
		System.out.println("Hola mundo");
		connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		System.out.println("Conectado a la base de datos");
		lectura(connection);
		while(true){
			Action action=menu();
			if(action == Action.Salir)break;
			if(action == Action.Nuevo)nuevo(connection);
			if(action == Action.Listar)lectura(connection);
			if(action == Action.Editar)actualizacion(connection);
			if(action == Action.Eliminar)borrado(connection);
			
		}
		System.out.println("");
		closePreparedStatement();
		connection.close();
	}
}
