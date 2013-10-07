package Exerc6;

import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;
import java.util.Random;

public class MyCryptoTool {
	private byte[] hashGood ;
	private byte[] hashBad ;
	
	public MyCryptoTool() throws NoSuchAlgorithmException, IOException{
		hashGood = produceHash("c:\\GoodApp.java");
		hashBad = produceHash("c:\\BadApp.java");
		
	}
	
	private byte[] produceHash(String filePath) throws NoSuchAlgorithmException, IOException {
		MessageDigest md =  produceMessageDigest(filePath);
		
		return md.digest();
	}
	
	private MessageDigest produceMessageDigest(String filePath) throws NoSuchAlgorithmException, IOException {
		MessageDigest md =  MessageDigest.getInstance("SHA1");
		InputStream is = new FileInputStream(filePath);
		
		byte[] data = new byte[4096];
		
		byte b; int i= 0;
		
		while ((b=(byte) is.read())!=-1 && i < data.length) {
			data[i] = b ;
			i++;
		}
		is.close();
		md.update(Arrays.copyOf(data, i));
		return md ;
	}
	

	private byte[] read(int nBits, byte[] h) {
		int nFullBytes = nBits / 8;
		byte[] result ;
		
		result = (nBits%8 > 0)?new byte[nFullBytes +1]:new byte[nFullBytes];
				
		if (nBits>=8) {
			System.arraycopy(h, 0, result, 0, nFullBytes);
			int remainingBits = nBits - (nFullBytes)*8;
			if(remainingBits==0)
				return result;
		
			//Para shiftar é necessário converter para int e colocar a zero todos os octetos
			//excepto o primeiro
			int x = ((int) h[nFullBytes]) & 0xFF;
			result[nFullBytes] = (byte) (x >> (8-remainingBits));
			return result;
		}
		int x = ((int) h[0]) & 0xFF;
		result[0] = (byte) (x >> (nBits));
		return result ;
		
	}
	
	public static void main(String[] args) throws NoSuchAlgorithmException, IOException {
		
		MyCryptoTool crypto = new MyCryptoTool();
		
		System.out.println("* Questão 5.1");
		System.out.println("Para 8 bits");
		crypto.printHashes(8);
		System.out.println("Para 14 bits");
		crypto.printHashes(16);
		System.out.println("Para 32 bits");
		crypto.printHashes(32);
		
		System.out.println("* Questão 5.2");
		
		int nOperations8 = 0, nOperations12 = 0, nOperations16 = 0, n = 0;
		while (n<10) {
			nOperations8 += crypto.findEqualHash(8);
			nOperations12 += crypto.findEqualHash(12) ;
			nOperations16 += crypto.findEqualHash(16);
			n+=1;
		}
		System.out.println("Média de operações para 08 bits: " + nOperations8/10);
		System.out.println("Média de operações para 12 bits: "+ nOperations12/10);
		System.out.println("Média de operações para 16 bits: "+ nOperations16/10);
		
	}
	
	private void printHashes( int nBits) 
	{
		System.out.print("GoodApp Hash: ");
		for (byte b : this.read(nBits,hashGood))
		{
			System.out.printf("%x", b);
		}
		System.out.print("\nBadApp Hash: ");
		for (byte b : this.read(nBits,hashBad))
		{
			System.out.printf("%x", b);
	
		}
		System.out.println();
	}
	
	private int findEqualHash(int nBits) throws NoSuchAlgorithmException, IOException {
		MessageDigest mdBadApp ;
		int nOperations = 0;
		byte[] input = new byte[4096];
		Random r = new Random(System.currentTimeMillis());

		while (!(Arrays.equals(read(nBits,hashGood),read(nBits,hashBad)))) {
			mdBadApp = produceMessageDigest("C:\\BadApp.java") ;
			r.nextBytes(input);
			input[0] = '/'; input[1] = '/';
			mdBadApp.update(input);
			hashBad = mdBadApp.digest() ;
			nOperations += 1 ;
		}
		
		
		
		return nOperations ;
	}
	
}
