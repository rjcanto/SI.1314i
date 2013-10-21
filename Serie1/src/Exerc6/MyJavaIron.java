package Exerc6;

import java.security.NoSuchAlgorithmException;

import javax.crypto.Cipher;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;

/**
 * 
 * @author Ricardo Canto
 * @author Luís Brás
 *
 * A small library to use Iron in Java
 * https://github.com/hueniverse/iron
 */

public class MyJavaIron {
	
	
	
	public MyJavaIron() {
		
	}
	
	public Cipher builtCipher(String obg, String op, int keyLen) {
		Cipher c ;
		return null;
	}
	/*teste*/
	public SecretKey generate(String password, IronOptions options) throws NoSuchAlgorithmException {
		SecretKeyFactory f = SecretKeyFactory.getInstance("PBKDF2");
		SecretKey k =f.generateKey();
		return null ;
	}
	
	public byte[] encrypt(SecretKey k, IronOptions options, String data) {
		return null;
	}
	
	public String decrypt(SecretKey k, IronOptions options) {
		return null;
	}

}
