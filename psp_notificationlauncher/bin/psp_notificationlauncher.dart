import 'dart:io';

void main() {
  print("Hello, World!");
  
  new HttpClient().getUrl(new Uri('http://pspapi.dannyschreiber.net/Site/GetAll'))
  .then((HttpClientRequest request){
	  print('sent request');
	  
  })
  .then((HttpClientResponse	response){
	 print(response); 
  });
}
