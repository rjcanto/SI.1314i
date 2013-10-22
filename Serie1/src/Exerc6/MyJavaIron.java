package Exerc6;

import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.KeySpec;
import java.util.Random;

import javax.crypto.Cipher;
import javax.crypto.SecretKey;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.PBEKeySpec;
import javax.crypto.spec.SecretKeySpec;

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

	public SecretKey generate(String password, IronOptions options) throws NoSuchAlgorithmException, InvalidKeySpecException {
		SecretKeyFactory f = SecretKeyFactory.getInstance("PBKDF2");
		Random r = new SecureRandom();
		r.nextBytes(options.salt); 
		
		
		KeySpec spec = new PBEKeySpec(password.toCharArray(),
										options.salt,
										options.iterations,
										options.saltBits
									);
		SecretKey k =f.generateSecret(spec);
		return new SecretKeySpec(k.getEncoded(),"AES");
	}
	
	public byte[] encrypt(SecretKey k, IronOptions options, String data) {
		return null;
	}
	
	public String decrypt(SecretKey k, IronOptions options) {
		return null;
	}
	
	public static void main(String[] args) throws NoSuchAlgorithmException, InvalidKeySpecException {
		final String password = "TESTE";
		final String jsonObj = "{\"course\":\"LEIC\",\"is_Optional\":true,\"name\":\"SI1314i\"}" ;
		
		MyJavaIron iron = new MyJavaIron() ;
		
		IronOptions io = new IronOptions();
		
		SecretKey sk = iron.generate(password,io);
		
		byte[] encryptionRes = iron.encrypt(sk, io, jsonObj) ;
		
		System.out.println(encryptionRes+"*"+io.salt+"*");
		
	}

}
