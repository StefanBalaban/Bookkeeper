FROM nginx:latest

ARG APP
ARG API
ENV APP "$APP"
ENV API "$API"
 
COPY ./nginx.conf.template /nginx.conf.template
CMD ["/bin/sh" , "-c" , "envsubst '$API $APP' < /nginx.conf.template > /etc/nginx/nginx.conf && exec nginx -g 'daemon off;'"]

EXPOSE 8082
EXPOSE 8083