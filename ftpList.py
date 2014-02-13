import urllib2

def try_open_uri(url):
    try:
        urllib2.urlopen(url)
        return True
    except urllib2.URLError:
        return False
#end def

base_url = 'ftp://mabipatch.nexon.net/%i/%s'
to_url = '%i_to_%i.txt'
full_url = '%i_full.txt'

SEARCH_DEPTH = 5
MIN_CLIENT_VERSION = 161
MAX_CLIENT_VERSION = 165

for version in range(MIN_CLIENT_VERSION, MAX_CLIENT_VERSION +1):
    for back_version in range(version - SEARCH_DEPTH, version):
        if try_open_uri(base_url % (version, to_url % (back_version, version))):
            print '%i to %i' % (back_version, version)
        #end if
    #end for
    if try_open_uri(base_url % (version, full_url % version)):
        print '%i full' % version
    #end if
#end if
