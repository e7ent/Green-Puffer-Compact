Parse.Cloud.define("hello", function(request, response) {
  response.success("Hello world!");
});


Parse.Cloud.define("provideReward", function (req, res) {

});

Parse.Cloud.afterSave(Parse.User, function(request) {
	if (request.object.existed() == true) return ;

	var Character = Parse.Object.extend("Character");
	//var defaultCharacter = 
	console.log("Exist : " + request.object.existed());
})