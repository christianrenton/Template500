if(!Sanitize.Config) {
  Sanitize.Config = {}
}

Sanitize.Config.BASIC = {
  elements: [
     'a', 'b', 'blockquote', 'br', 'cite', 'code', 'div', 'dd', 'dl', 'dt', 'em',
     'i', 'img', 'li', 'ol', 'p', 'pre', 'q', 'small', 'strike', 'strong', 'sub',
     'sup', 'u', 'ul'],

   attributes: {
     'a'         : ['href'],
     'blockquote': ['cite'],
     'img'       : ['src'],
     'q'         : ['cite']
   },

   add_attributes: {
     'a': {'rel': 'nofollow'}
   },

   protocols: {
     'a'         : {'href': ['ftp', 'http', 'https', 'mailto',
                                 Sanitize.RELATIVE]},
     'blockquote': {'cite': ['http', 'https', Sanitize.RELATIVE]},
     'q'         : {'cite': ['http', 'https', Sanitize.RELATIVE]}
   }
}
