package Exerc6;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.security.DigestInputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;

public class MyCryptoTool {
	byte[] hashGood ;
	byte[] hashBad ;
	
	public MyCryptoTool() throws NoSuchAlgorithmException, IOException{
		hashGood = produceHash("c:\\GoodApp.java");
		hashBad = produceHash("c:\\BadApp.java");
		
	}
	
	private byte[] produceHash(String filePath) throws NoSuchAlgorithmException, IOException {
		MessageDigest md =  MessageDigest.getInstance("SHA1");
		InputStream is = new FileInputStream(filePath);
		
		
		
		byte[] data = new byte[4096];
		
		byte b; int i= 0;
		
		while ((b=(byte) is.read())!=-1 && i < data.length) {
			data[i] = b ;
			i++;
		}
		is.close();
		return md.digest();
	}
	
	private byte[] readGood(int nBytes){
		return Arrays.copyOf(hashBad, nBytes);
		
	}
	
	private byte[] readBad(int nBytes){
		
		
		return Arrays.copyOf(hashGood, nBytes);
			
	}
	
	public static void main(String[] args) throws NoSuchAlgorithmException, IOException {
		MyCryptoTool crypto = new MyCryptoTool();
		int nBytes = 1 ;
		
		System.out.println("GoodApp Hash: " + crypto.readGood(nBytes).toString()) ;
		System.out.println("BadApp Hash: " + crypto.readBad(nBytes).toString()) ;
	}
}
