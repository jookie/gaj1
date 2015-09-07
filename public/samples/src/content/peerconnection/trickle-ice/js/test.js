'use strict';
// This is a basic test file for use with testling.
// The test script language comes from tape.
/* jshint node: true */
var test = require('tape');

var webdriver = require('selenium-webdriver');
var seleniumHelpers = require('../../../../../test/selenium-lib');

test('Candidate Gathering', function(t) {
  var driver = seleniumHelpers.buildDriver();

  driver.get('file://' + process.cwd() +
      '/src/content/peerconnection/trickle-ice/index.html')
  .then(function() {
    t.pass('page loaded');
    return driver.findElement(webdriver.By.id('gather')).click();
  })
  .then(function() {
    return driver.wait(function() {
      return driver.executeScript(
          'return pc === null && candidates.length > 0;');
    }, 30 * 1000);
  })
  .then(function() {
    t.pass('got candidates');
    driver.close();
    driver.quit();
    t.end();
  })
  .then(null, function(err) {
    t.fail(err);
    driver.close();
    driver.quit();
    t.end();
  });
});

test('Loading server data', function(t) {
  var driver = seleniumHelpers.buildDriver();

  driver.get('file://' + process.cwd() +
      '/src/content/peerconnection/trickle-ice/index.html')
  .then(function() {
    t.pass('page loaded');
    return driver.findElement(webdriver.By.css('#servers>option'));
  })
  .then(function(element) {
    return new webdriver.ActionSequence(driver).
        doubleClick(element).perform();
  })
  .then(function() {
    return driver.findElement(webdriver.By.id('url')).getAttribute('value');
  })
  .then(function(value) {
    t.ok(value !== '', 'doubleclick loads server data');
    driver.close();
    driver.quit();
    t.end();
  })
  .then(null, function(err) {
    t.fail(err);
    driver.close();
    driver.quit();
    t.end();
  });
});

test('Adding a server', function(t) {
  var driver = seleniumHelpers.buildDriver();

  driver.get('file://' + process.cwd() +
      '/src/content/peerconnection/trickle-ice/index.html')
  .then(function() {
    t.pass('page loaded');
    return driver.findElement(webdriver.By.id('url'))
        .sendKeys('stun:stun.l.google.com:19302');
  })
  .then(function() {
    t.pass('url input worked');
    return driver.findElement(webdriver.By.id('add')).click();
  })
  .then(function() {
    return driver.findElement(webdriver.By.css('#servers'))
        .getAttribute('length');
  })
  .then(function(length) {
    t.ok(length === '2', 'server added');
    driver.close();
    driver.quit();
    t.end();
  })
  .then(null, function(err) {
    t.fail(err);
    driver.close();
    driver.quit();
    t.end();
  });
});

test('Removing a server', function(t) {
  var driver = seleniumHelpers.buildDriver();

  driver.get('file://' + process.cwd() +
      '/src/content/peerconnection/trickle-ice/index.html')
  .then(function() {
    t.pass('page loaded');
    return driver.findElement(webdriver.By.css('#servers>option')).click();
  })
  .then(function() {
    return driver.findElement(webdriver.By.id('remove')).click();
  })
  .then(function() {
    return driver.findElement(webdriver.By.css('#servers'))
        .getAttribute('length');
  })
  .then(function(length) {
    t.ok(length === '0', 'server removed');
    driver.close();
    driver.quit();
    t.end();
    driver = null;
  })
  .then(null, function(err) {
    t.fail(err);
    if (driver) {
      driver.close();
      driver.quit();
      driver = null;
    }
    t.end();
  });
});
