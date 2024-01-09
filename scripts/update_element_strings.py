import requests
import os
import json
import xml.etree.ElementTree as ElementTree
from xml.dom import minidom

supported_locales = ['de', 'en', 'es', 'fr', 'nl', 'pt', 'se']
base_url = 'https://raw.githubusercontent.com/ory/elements/main/src/locales/{locale}.json'


# Function to convert JSON to .resx format
def convert_to_resx(json_data, locale):
    root = ElementTree.Element('root')

    for key, value in json_data.items():
        data = ElementTree.SubElement(root, 'data')
        ElementTree.SubElement(data, 'value').text = value
        ElementTree.SubElement(data, 'comment').text = key
        data.set('name', key)
        data.set('xml:space', 'preserve')

    xml_string = ElementTree.tostring(root, 'utf-8')

    # Format the XML string
    xml_dom = minidom.parseString(xml_string)
    formatted_xml = xml_dom.toprettyxml(indent="  ")

    filename = None
    if locale is 'en':
        filename = f'../KratosSelfService/Resources/OryElementsTranslator.resx'
    else:
        filename = f'../KratosSelfService/Resources/OryElementsTranslator.{locale}.resx'
        
    with open(filename, 'w', encoding='utf-8') as resx_file:
        resx_file.write(formatted_xml)


if __name__ == '__main__':
    if not os.path.exists('Resources'):
        os.makedirs('Resources')

    # Iterate through supported locales and generate .resx files
    for locale in supported_locales:
        url = base_url.format(locale=locale)
        response = requests.get(url)

        if response.status_code == 200:
            convert_to_resx(response.json(), locale)
            print(f'Converted {locale} to .resx')
        else:
            print(f'Failed to fetch data for {locale}')

    print('Localization update completed.')
