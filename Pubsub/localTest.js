var system = require('system');
var host = system.env['CASPERJS11_URL'];

currentCount = 0;

casper.test.begin('Test Pubsub sample.', 3, function suite(test) {
    casper.start(host + '/', function (response) {
        console.log('Starting ' + host + '/');
        test.assertSelectorHasText('H1', 'Google AppEngine Pubsub Sample');
        this.fill('#MessageForm', {
            'Message': 'halcyonine',
        }, false);
        console.log("Filled form.");
    });

    casper.thenClick('#Submit', function (response) {
        console.log('Submitted form.');
        test.assertEquals(200, response.status);
        test.assertSelectorHasText('#PublishedMessage', 'halcyonine');
    });

    // TODO: test messages get received, somehow.

    casper.run(function () {
        test.done();
    });
});
