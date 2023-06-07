import xmltodict
import datetime

class medicalDocument:
    def __init__(self):
        self.age = None
        self.gender = None  # 1-М, 2-Ж
        self.mainDisease = None  # Код заболевания
        self.instrumentalText = None
        self.laboratoryText = None
        self.morphologyText = None
        self.consultationText = None
        self.drugText = None
        self.nonDrugText = None
        self.surgicalText = None
        self.dietText = None
        self.treatText = None
        self.workText = None

    def parseXML(self, filePath):
        f = open(filePath, 'r', encoding='utf-8')
        xml = f.read()
        d = xmltodict.parse(xml)

        self.age = datetime.date.today().year - datetime.datetime.strptime(
            d['ClinicalDocument']['recordTarget']['patientRole']['patient']['birthTime']['@value'],'%Y%m%d').year
        self.gender = int(
            d['ClinicalDocument']['recordTarget']['patientRole']['patient']['administrativeGenderCode']['@code'])

        sections = d['ClinicalDocument']['component']['structuredBody']['component']

        for s in sections:
            self.parseSection(s['section'])

    def parseSection(self, section):
        code = section['code']['@code']
        match code:
            case 'HOSP':
                self.parseHOSP(section)
            case _:
                if 'component' not in section:
                    return

                self.parseComponentsText(section['component'])

    def parseHOSP(self, section):
        #ОБЩИЕ ДАННЫЕ О ГОСПИТАЛИЗАЦИИ
        entry = None

        # Может быть целый список entry, из них нужно найти подходящий
        for e in section['entry']:
            if 'act' not in e:
                continue

            if e['act']['@classCode'] == 'ACT':
                entry = e
                break

        if entry is None:
            return

        self.mainDisease = entry['act']['entryRelationship']['observation']['value']['@code']

    def parseComponentsText(self, section):
        for c in section:
            text = c['section']['text']
            code = c['section']['code']['@code']

            match code:
                case 'RESINSTR': self.instrumentalText = text
                case 'RESLAB': self.laboratoryText = text
                case 'RESMOR': self.morphologyText = text
                case 'RESCONS': self.consultationText = text
                case 'DRUG': self.drugText = text
                case 'NONDRUG': self.nonDrugText = text
                case 'SUR': self.surgicalText = text
                case 'RECDIET': self.dietText = text
                case 'RECTREAT': self.treatText = text
                case 'RECWORK': self.workText = text

    def printInfo(self):
        print()
        print('Данные пациента:')
        print('Возраст: {0} лет'.format(self.age))
        print('Пол: {0}'.format({1: 'Муж', 2: 'Жен'}[self.gender]))
        print('Осн. заболевание: {0}'.format(self.mainDisease))
        print('Инструментальные исследования:\n\t{0}'.format(self.instrumentalText))
        print('Лабораторные исследования:\n\t{0}'.format(self.laboratoryText))
        print('Морфологические исследования:\n\t{0}'.format(self.morphologyText))
        print('Консультация врача:\n\t{0}'.format(self.consultationText))
        print('Медикаментозное лечение:\n\t{0}'.format(self.drugText))
        print('Немедикаментозное лечение:\n\t{0}'.format(self.nonDrugText))
        print('Хирургическое вмешательство:\n\t{0}'.format(self.surgicalText))
        print('Диета:\n\t{0}'.format(self.dietText))
        print('Лечение:\n\t{0}'.format(self.treatText))
        print('Трудовые рекомендации:\n\t{0}'.format(self.workText))


doc = medicalDocument()

doc.parseXML('Data\\medicalDocumentХТЭЛГ.xml')
doc.printInfo()

