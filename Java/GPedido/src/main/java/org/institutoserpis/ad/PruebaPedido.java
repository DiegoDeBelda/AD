package org.institutoserpis.ad;
import java.util.List;
import java.util.logging.Level;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Persistence;

import java.util.logging.Logger;
public class PruebaPedido {

	private static EntityManagerFactory entityManagerFactory;
	public static void main(String[] args) {
			Logger.getLogger("org.hibernate").setLevel(Level.SEVERE);
			System.out.println("inicio");
			entityManagerFactory = Persistence.createEntityManagerFactory("org.institutoserpis.ad");
			

			EntityManager entityManager = entityManagerFactory.createEntityManager();
			entityManager.getTransaction().begin();
			List<Categoria> Categorias = entityManager.createQuery("from Categoria", Categoria.class).getResultList();
			for(Categoria cat : Categorias)
				System.out.println(cat);
			entityManager.getTransaction().commit();
			entityManager.close();
					
	}
	

}