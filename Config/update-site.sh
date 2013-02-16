#!/bin/sh

cd /srv/www/portfoliodam
git reset --hard HEAD
git pull
/etc/init.d/apache2 reload