angular.module('ui.splash', ['ui.bootstrap'])
    .service('$splash', [
  '$modal',
  '$rootScope',
  function ($modal, $rootScope) {
            return {
                open: function (attrs, opts) {
                    // Create a new scope so we can pass custom
                    // variables into the splash modal
                    var scope = $rootScope.$new();
                    angular.extend(scope, attrs);
                    opts = angular.extend(opts || {}, {
                        backdrop: false,
                        scope: scope,
                        templateUrl: 'splash/content.html',
                        windowTemplateUrl: 'splash/index.html'
                    });
                    return $modal.open(opts);
                }
            };
  }
]).run([
  '$templateCache',
  function ($templateCache) {
            $templateCache.put('splash/index.html',
                '<section class="splash" ng-class="{\'splash-open\': animate}" ng-style="{\'z-index\': 1000, display: \'block\'}" ng-click="close($event)">' +
                '  <div class="splash-inner" ng-transclude></div>' +
                '</section>'
            );
            $templateCache.put('splash/content.html',
                '<div dir="ltr" class="splash-content text-center">' +
                '  <h1 ng-bind="title"></h1>' +
                '  <p class="lead" ng-bind="message"></p>' +
                '  <img src="/assets/Octopus.png"  ng-click="$close()"/>' +
                '</div>'
            );
  }
]);