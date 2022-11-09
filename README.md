# lawyer-crud

## Setup inicial
Para esse projeto, foi utilizada a versão do .net SDK 6.0.

Para instalar essa versão são necessários os seguintes comandos:  

    wget https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb

    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb

    sudo apt-get update && \
    sudo apt-get install -y dotnet-sdk-6.0
  
Após isso, a aplicação pode ser executada com o seguinte comando:  

    dotnet run
