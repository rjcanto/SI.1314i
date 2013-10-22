package Exerc6;

public class IronOptions {

	//Primitiva
	static final int DES=0, AES=1;
		
	//Operation Mode
	static final int CBC=0, CTR=1;

	
	protected int saltBits;
    protected byte[] salt;
    protected String algorithm;
    protected int iterations;
    protected byte[] iv;
    	
	public IronOptions() {
		saltBits = 256;
		salt = new byte[20];
		
	}
}
