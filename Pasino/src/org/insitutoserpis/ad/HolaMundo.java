

import java.sql.Connection;

public class HolaMundo {
	public static void main(String args [])throws SQLexception{
		System.out.println("Hola mundo");
		Connection connection = DriveManager.getConnection(
				"jdbc:mysql://localhost/dbprueba", "root", "sistemas");
		connection.close();
		System.out.println("fin");
		
	}
}
