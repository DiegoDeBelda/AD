package org.institutoserpis.ad;
import java.util.Date;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

 

public class Pruebaarticulo {

	static EntityManagerFactory entityManagerFactory ; 
			
	public static void main(String[] args) {
		Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
		System.out.println("inicio");
		entityManagerFactory = 
				Persistence.createEntityManagerFactory("org.institutoserpis.ad");
		
//		Long articuloId = persist();
//		find(articuloId);
//		update(articuloId);
//		remove(articuloId);
		query();
		entityManagerFactory.close();
		
		
	}
	private static void query(){
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		List<Articulo> Articulos = entityManager.createQuery("from Articulo", Articulo.class).getResultList();
		for(Articulo art : Articulos)
			show(art);
		entityManager.getTransaction().commit();
		entityManager.close();
	}
	private static void show(Articulo art) {
		System.out.printf("%5s %-40s %25s %10s\n", 
				art.getId(), 
				art.getNombre(), 
				formatCategoria(art.getCategoria()), 
				art.getPrecio()+"€"
		);
		
	}
	
	private static String formatCategoria(Categoria categoria){
		if(categoria==null){
			return null;
		}
		else{
			return String.format("%4s %-20s",categoria.getId(), categoria.getNombre());
		}
		
		
		
	}
	private static Long persist(){
		 
		
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo articuloNuevo = new Articulo();
		articuloNuevo.setNombre("nuevo"+new Date());
		entityManager.persist(articuloNuevo);
		entityManager.getTransaction().commit();
		entityManager.close();
		show(articuloNuevo);
		return articuloNuevo.getId();
	}
	
	private static void find(Long id){
		System.out.println("find: "+id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo art = entityManager.find(Articulo.class,id);
		entityManager.persist(art);
		entityManager.getTransaction().commit();
		entityManager.close();
		show(art);
	}
	private static void update(Long id){
		System.out.println("update: "+id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo art = entityManager.find(Articulo.class, id);
		art.setNombre("Modificado"+new Date());
		entityManager.getTransaction().commit();
		entityManager.close();
		show(art);
	}
	private static void remove(Long id){
		System.out.println("remove: "+id);
		EntityManager entityManager = entityManagerFactory.createEntityManager();
		entityManager.getTransaction().begin();
		Articulo art = entityManager.find(Articulo.class, id);
		entityManager.remove(art);
		entityManager.getTransaction().commit();
		entityManager.close();
	}

}