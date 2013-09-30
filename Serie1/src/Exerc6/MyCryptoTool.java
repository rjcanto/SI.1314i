package Exerc6;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.InputStream;
import java.security.DigestInputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class MyCryptoTool {
	MessageDigest md ;
	DigestInputStream dis;
	InputStream is;
	
	public MyCryptoTool() throws NoSuchAlgorithmException, FileNotFoundException{
		md =  MessageDigest.getInstance("SHA1");
		is = new FileInputStream("GoodApp.java");
		dis = new DigestInputStream(is, md);
		byte[] hash = dis.getMessageDigest().digest();
		
		for(byte b: hash)
			System.out.println(String.format("%x", b));
		
		System.out.println("---");
	}
}
