package exerc1;

import java.io.IOException;
import java.net.UnknownHostException;
import java.security.cert.Certificate;
import java.security.cert.X509Certificate;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import javax.net.ssl.SSLSocket;
import javax.net.ssl.SSLSocketFactory;



public class CertValidator {

	public List<X509Certificate> certInChainExpiresInLessThan(int days, String uri, int port) throws UnknownHostException, IOException {
		
		SSLSocketFactory socketFactory = (SSLSocketFactory) SSLSocketFactory.getDefault();
		
		SSLSocket socket = null;
		
		try {
			socket = (SSLSocket) socketFactory.createSocket(uri, port);
			socket.startHandshake();
			
			Calendar cal = Calendar.getInstance();
			cal.add(Calendar.DAY_OF_MONTH, days);
			Date expirationDate = cal.getTime();
			
			List<X509Certificate> expiredCerts = new ArrayList<>();
			
			Certificate[] serverCerts = socket.getSession().getPeerCertificates();
			for (Certificate cert : serverCerts)
			{
				X509Certificate x509Cert = (X509Certificate) cert;
				System.out.println("**************************");
				System.out.println("Name: " + x509Cert.getSubjectX500Principal().getName());
				System.out.println("Expiration date: " + x509Cert.getNotAfter().toString());
				
				if (expirationDate.after(x509Cert.getNotAfter())) {
					expiredCerts.add(x509Cert);
				}
			}
			
			System.out.println();
			return expiredCerts;
		}
		finally {
			if (socket != null) {
				socket.close();
			}
		}
	}
	
}
