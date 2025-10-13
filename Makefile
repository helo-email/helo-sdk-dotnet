generate:
	kiota generate -l CSharp -c HeloApiClient -n Helo.ApiClient -d ../helo-web/vendor/schemas/helo.json -o ./src/Helo.ApiClient