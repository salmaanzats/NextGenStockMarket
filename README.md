Next Gen Stock Market Game

We have developed stock market simulation game using web api and frontend using angular.

Repository:
	url : https://github.com/salmaanzats/NextGenStockMarket
	
Kanban Board : 
	url : https://github.com/salmaanzats/NextGenStockMarket/projects/1
	
Sprint Planning:
	location : NextGenStockMarket\documents\NextGen - SCRUM Documentation
	
Demo: 1 minute video about how game works(using 2 turns and two players)
	location : 

Web API(.NET) - Using Asynchronous programming
	location : NextGenStockMarket\Code\NextGenStockMarketAPI\NextGenStockMarketAPI.sln
	Hosted Site : http://nextgenstocksimulationapi.somee.com
	
Angular:
	location : NextGenStockMarket\Code\Presentation\NextGenStockMarket
	
#############################################################################################

How to run:
	pre-requisities:
		-> Visual studio with .NET Framework 4.6
		-> Node.js
		
	1.Open the web API project and Run(it will restore all the necessary packages and will build the games backend) 
											or
	  visit http://nextgenstocksimulationapi.somee.com
		
	2.Then navigate to frontend code location(NextGenStockMarket\Code\Presentation\NextGenStockMarket) from cmd or powershell type npm install it install
	  nodemodule which declared in the package.json
	3.Then navigate to frontend code location(NextGenStockMarket\Code\Presentation\NextGenStockMarket) from cmd or powershell type npm start then it	
	  will start the game.
	4.Creating multiple instances: Simply open a new tab using fronend url.(for 4 player have to open four pages and register them)
	
	
##############################################################################################

How to publish & run : 
    1. Open Internet Information Services (IIS) Manager (To open IIS Manager Press windows key + R --->type 'intmgr'--->click ok)
    2. In the 'Connections' pane there was a collapse folder in your PC name
    3. Click on that & expand the it.It contains folder which called the 'Sites'.
    4. Right click on that folder & select 'Add website'.
   
    ****************To publish the Api****************
    5. Enter a name for the 'Site name' field. Ex: NxtGen Api
    6. Then give the physical path of the 'NextGEnStockMarket/publish/api' folder (ex : D/Project/NextGEnStockMarket/publish/api)
    7. Then click on the connect as button & select the 'Specific user' & click 'Set...' button.
    8. Then enter your PC user name, password for the user name field & password , confirm password fields.
    9. Then set the port number to 8081 & click on 'Ok'.
    10. Then the host name will appear on the 'Actions' pane in the right side & click on the link to open the host api.
    
    ****************To publish the Frontend****************
    11. Enter a name for the 'Site name' field.  Ex: NxtGen Front end
    12. Then give the physical path of the 'NextGEnStockMarket/publish/frontend' folder 
            (ex : D/Project/NextGEnStockMarket/publish/frontend)
    13. Then click on the connect as button & select the 'Specific user' & click 'Set...' button.
    14. Then enter your PC user name, password for the user name field & password , confirm password fields.
    15. Then set the port number to 8082 & click on 'Ok'.
    16. Then the host name will appear on the 'Actions' pane in the right side & click on the link to open the host api.
	
